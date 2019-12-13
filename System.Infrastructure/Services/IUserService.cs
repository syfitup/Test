using SYF.Infrastructure.Models;
using SYF.Infrastructure.Models.Requests;
using SYF.Infrastructure.Models.Responses;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SYF.Services
{
    public interface IUserService: IService
    {
        Task<UserModel> GetByIdAsync(Guid id);
        Task<List<UserSummary>> SearchAsync(UserSearchRequest request);
        Task<AuthenticateResponse> AuthenticateAsync(AuthenticateRequest request);
        Task<CreateResponse> CreateAsync(UserModel model);
    }
}
