using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SYF.Infrastructure.Models;
using SYF.Infrastructure.Models.Requests;
using SYF.Infrastructure.Models.Responses;
using SYF.Services;

namespace SYF.Infrastructure.Services
{
    public interface ISubDepartmentService : IService
    {
        Task<SubDepartmentModel> GetByIdAsync(Guid id);
        Task<IEnumerable<SubDepartmentModel>> SearchAsync(SubDepartmentSearchRequest request);
        Task<CreateResponse> CreateAsync(SubDepartmentModel model);
        Task UpdateAsync(Guid id, SubDepartmentModel model);
        Task DeleteAsync(Guid id);
    }
}

