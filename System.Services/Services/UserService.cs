using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SYF.Infrastructure.Models.Responses;
using SYF.Infrastructure.Models.Requests;
using SYF.Infrastructure.Exceptions;
using SYF.Infrastructure.Enums;
using Newtonsoft.Json;
using SYF.Infrastructure.Criteria;
using System.Linq;
using SYF.Services.Services;
using SYF.Data;
using SYF.Infrastructure.Providers;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using SYF.Framework;
using SYF.Infrastructure.Entities;
using SYF.Infrastructure.Models;
using AutoMapper;
using AutoMapper.QueryableExtensions;

namespace SYF.Services
{
    public class UserService : BaseService, IUserService
    {
        private readonly IMapper _mapper;

        public UserService(ISecurityProvider securityProvider, DataContext context, IServiceProvider serviceProvider, IClaimsPrincipalProvider principalProvider, ILoggerFactory loggerFactory, IMapper mapper)
           : base(context, serviceProvider, principalProvider, loggerFactory)
        {
            SecurityProvider = securityProvider;
            _mapper = mapper;
        }

        private ISecurityProvider SecurityProvider { get; }
        public async Task<List<UserSummary>> SearchAsync(UserSearchRequest request)
        {
            var criteria = _mapper.Map<UserCriteria>(request);
            criteria.Deleted = false;

            return await DataContext.Users
                .AsNoTracking()
                .Include(x => x.Person.Department)
                .Include(x => x.Person.SubDepartment)
                .Include(x => x.Person.PersonPosition)
                .Query(criteria)
                .OrderBy(x => x.UserName)
                .Select( x => _mapper.Map<UserSummary>(x))
                .ToListAsync();
        }
        public async Task<UserModel> GetByIdAsync(Guid id)
        {
            var entity = await DataContext.Users
                .AsNoTracking()
                .Include(a => a.Person)
                .FirstOrDefaultAsync(x => x.Id == id);

            var model = _mapper.Map<UserModel>(entity);

            model.Roles = await DataContext.PersonRoles
                .AsNoTracking()
                .Include(a => a.Role)
                .Where(x => x.PersonId == model.PersonId && !x.Deleted)
                .ProjectTo<PersonRoleModel>(_mapper.ConfigurationProvider)
                .ToListAsync();

            model.Access = await DataContext.PersonAccess
                .AsNoTracking()
                .Include(a => a.Department)
                .Include(a => a.SubDepartment)
                .Where(x => x.PersonId == model.PersonId && !x.Deleted)
                .ProjectTo<PersonAccessModel>(_mapper.ConfigurationProvider)
                .ToListAsync();


            return model;
        }

        public async Task<AuthenticateResponse> AuthenticateAsync(AuthenticateRequest request)
        {
            var criteria = new UserCriteria { UserName = request.Email, Deleted = false };
            var user = await DataContext.Users.Query(criteria)
                .Include(x => x.Person)
                .FirstOrDefaultAsync();

            // User does not exist
            if (user == null)
            {
                await CreateLoginHistory(null, request.Email, request.DomainName, request.SuperUser, LoginResults.InvalidUserName);

                throw new AuthenticateException { UserName = request.Email, Reason = AuthenticateFailureReason.InvalidCredentials };
            }

            // User is flagged as a system account
            if (user.Flags.HasFlag(UserFlags.SystemAccount))
            {
                await CreateLoginHistory(user.Id, request.Email, request.DomainName, request.SuperUser, LoginResults.SystemAccount);

                throw new AuthenticateException { UserName = request.Email, Reason = AuthenticateFailureReason.SystemAccount };
            }

            // User is flagged as disabled
            if (user.Flags.HasFlag(UserFlags.Disabled))
            {
                await CreateLoginHistory(user.Id, request.Email, request.DomainName, request.SuperUser, LoginResults.Disabled);

                throw new AuthenticateException { UserName = request.Email, Reason = AuthenticateFailureReason.Disabled };
            }

            if (user.Password == null || user.PasswordSalt == null)
            {
                await CreateLoginHistory(user.Id, request.Email, request.DomainName, request.SuperUser,
                LoginResults.PasswordNotSet);

                throw new AuthenticateException
                {
                    UserName = request.Email,
                    Reason = AuthenticateFailureReason.InvalidCredentials
                };
            }

            var hasPasswordBug = user.Flags.HasFlag(UserFlags.PasswordBug);
            if (request.Password == null) request.Password = "";
            var checkPassword =
                PasswordHelper.EncryptPassword(request.Password, user.PasswordSalt, hasPasswordBug);
            if (user.Password != checkPassword)
            {
                await CreateLoginHistory(user.Id, request.Email, request.DomainName, request.SuperUser,
                    LoginResults.InvalidPassword);

                await ProcessFailedLoginAttemptAsync(user);

                throw new AuthenticateException
                {
                    UserName = request.Email,
                    Reason = AuthenticateFailureReason.InvalidCredentials
                };
            }


            // Check if the account is locked out
            if (user.Flags.HasFlag(UserFlags.Locked))
            {
                await CreateLoginHistory(user.Id, request.Email, request.DomainName, request.SuperUser, LoginResults.LockedOut);

                var locked = CheckLockoutDurationAsync(user);
                if (locked)
                {
                    throw new AuthenticateException
                    {
                        UserName = user.UserName,
                        Reason = AuthenticateFailureReason.LockedOut
                    };
                }
            }

            // Set values for successful authentication
            user.LastLoginDate = DateTime.UtcNow;
            user.FailedPasswordCount = 0;
            user.Flags &= ~UserFlags.Locked;
            user.LockoutDate = null;

            await DataContext.SaveChangesAsync();

            // Get the person roles
            var roles = await DataContext.PersonRoles
                .AsNoTracking()
                .Where(x => x.PersonId == user.PersonId && !x.Deleted && !x.Role.Deleted)
                .Select(x => x.Role)
                .ToArrayAsync();

            var tokenRequest = new SecurityTokenRequest
            {
                Id = user.Id,
                PersonId = user.PersonId,
                Name = user.UserName,
                SuperUser = request.SuperUser,
                Roles = roles.Select(r => r.Id).Distinct().ToArray()
            };
            var token = SecurityProvider.CreateToken(tokenRequest);

            // Get the list of permissions
            IEnumerable<string> permissions = new List<string>();
            foreach (var role in roles)
            {
                var rolePermissions = JsonConvert.DeserializeObject<string[]>(role.Permissions);
                permissions = permissions.Union(rolePermissions);
            }

            // Convert roles to string array
            var roleValues = roles.Select(x => x.Id.ToString()).ToArray();

            return new AuthenticateResponse
            {
                UserId = user.Id,
                PersonId = user.PersonId,
                Name = user.UserName,
                DisplayName = user.Person?.Name,
                PersonPositionName = "",
                Token = token,
                Roles = roleValues,
                Permissions = permissions.ToArray(),
            };
        }

        private async Task CreateLoginHistory(Guid? userId, string userName, string domainName, bool superUser, LoginResults result)
        {
            await DataContext.Database.ExecuteSqlInterpolatedAsync($"INSERT INTO dbo.UserLoginHistory (UserId, UserName, DomainName, IsSuperUser, LoginResult, HistoryDate) VALUES ({(object)userId ?? DBNull.Value}, {userName}, {domainName}, {superUser}, {(int)result}, {DateTime.UtcNow})");
        }

        private async Task ProcessFailedLoginAttemptAsync(User user)
        {
            user.FailedPasswordCount++;
            var lockedoutThreshold = 3;
            if (user.FailedPasswordCount >= lockedoutThreshold)
            {
                user.LockoutDate = DateTime.UtcNow;
                user.Flags |= UserFlags.Locked;
            }
            await DataContext.SaveChangesAsync();
        }
        private bool CheckLockoutDurationAsync(User user)
        {
            if (user.LockoutDate == null) return false;

            var lockoutDuration = 15;
            var userLockoutDuration = DateTime.UtcNow.Subtract((DateTime)user.LockoutDate).TotalMinutes;

            return userLockoutDuration < lockoutDuration;
        }

        public async Task<CreateResponse> CreateAsync(UserModel model)
        {
            if (await CheckExistsAsync(Guid.Empty, model.UserName))
                throw new InvalidOperationException("User Already exists");

            model.PersonId = SequentialGuid.NewGuid();

            var token = PasswordHelper.GenerateToken();
            var user = new User
            {
                Id = SequentialGuid.NewGuid(),
                Token = token,
                TokenDate = DateTime.UtcNow.AddMonths(1),
                PersonId = model.PersonId,
                Person = new Person
                {
                    Id = model.PersonId,
                    EmailAddress = model.EmailAddress,
                    MobileNumber = model.MobileNumber,
                    Name = model.Name,
                    PhoneNumber = model.PhoneNumber,

                },
                UserName = model.UserName,
                Flags = UserFlags.None,
            };

            DataContext.Users.Add(user);

            MapToEntity(model, user);

            await DataContext.SaveChangesAsync();

            //await SendNewUserNotificationAsync(user);

            return new CreateResponse { Id = user.Id };
        }

        private async Task<bool> CheckExistsAsync(Guid id, string userName)
        {
            return await DataContext.Users
                .AsNoTracking()
                .AnyAsync(x => x.Id != id && x.UserName == userName  && !x.Deleted);
        }

        public User MapToEntity(UserModel model, User entity)
        {
           var t = _mapper.Map<UserModel>(entity);


            // Save the user access
            foreach (var accessModel in model.Access)
            {
                PersonAccess accessObj = null;
                if (accessModel.Id != Guid.Empty)
                    accessObj = entity.Person.Access.FirstOrDefault(x => x.Id == accessModel.Id);

                if (accessObj == null)
                {
                    accessObj = new PersonAccess()
                    {
                        Id = SequentialGuid.NewGuid(),
                        PersonId = entity.PersonId,
                    };
                    entity.Person.Access.Add(accessObj);
                }

                //Mapper.Map(accessModel, accessObj);
            }

            //// Save the roles
            foreach (var roleModel in model.Roles)
            {
                PersonRole roleObj = null;
                if (roleModel.Id != Guid.Empty)
                    roleObj = entity.Person.Roles.FirstOrDefault(x => x.Id == roleModel.Id);

                if (roleObj == null)
                {
                    roleObj = new PersonRole
                    {
                        Id = SequentialGuid.NewGuid(),
                        PersonId = entity.PersonId
                    };
                    entity.Person.Roles.Add(roleObj);
                }

                roleObj.RoleId = roleModel.RoleId;
                roleObj.Deleted = roleModel.Deleted;
            }

            return entity;
        }

    }
}
    

