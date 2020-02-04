using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SYF.Infrastructure.Entities;
using SYF.Infrastructure.Models.Responses;
using SYF.Infrastructure.Providers;
using SYF.Framework;
using SYF.Infrastructure.Services;
using SYF.Services.Services;
using SYF.Data;
using SYF.Infrastructure.Models;
using SYF.Infrastructure.Models.Requests;
using SYF.Infrastructure.Criteria;
using SYF.Infrastructure.Exceptions;

namespace SYF.Services.Service
{
    public class SubDepartmentService : BaseService, ISubDepartmentService
    {
        private readonly IMapper _mapper;

        public SubDepartmentService(DataContext context, IServiceProvider serviceProvider, IClaimsPrincipalProvider principalProvider, ILoggerFactory loggerFactory, IMapper mapper)
            : base(context, serviceProvider, principalProvider, loggerFactory)
        {
            _mapper = mapper;
        }

        public async Task<SubDepartmentModel> GetByIdAsync(Guid id)
        {
            var model = await DataContext.SubDepartments
                .AsNoTracking()
                .Include(x => x.Department)
                .FirstOrDefaultAsync(x => x.Id == id);

            return _mapper.Map<SubDepartmentModel>(model);
        }

        public async Task<IEnumerable<SubDepartmentModel>> SearchAsync(SubDepartmentSearchRequest request)
        {
            var criteria = _mapper.Map<SubDepartmentCriteria>(request);
            criteria.Deleted = false;

            return await DataContext.SubDepartments
                .AsNoTracking()
                .Include(x => x.Department)
                .Query(criteria)
                .OrderBy(x => x.Name)
                .Select(x => _mapper.Map<SubDepartmentModel>(x))
                .ToListAsync();
        }

        public async Task<CreateResponse> CreateAsync(SubDepartmentModel model)
        {
            var entity = new SubDepartment
            {
                Id = SequentialGuid.NewGuid()
            };

            DataContext.SubDepartments.Add(entity);

            _mapper.Map(model, entity);

            await DataContext.SaveChangesAsync();

            return new CreateResponse { Id = entity.Id };
        }

        public async Task UpdateAsync(Guid id, SubDepartmentModel model)
        {
            var entity = await DataContext.Departments.FindAsync(id);
            if (entity == null) throw new NotFoundException();

            _mapper.Map(model, entity);
            await DataContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var entity = await DataContext.SubDepartments.FindAsync(id);
            if (entity == null) throw new NotFoundException();

            entity.Deleted = true;
            await DataContext.SaveChangesAsync();
        }
    }
}