using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SYF.Infrastructure.Models;
using SYF.Infrastructure.Models.Requests;
using SYF.Infrastructure.Models.Responses;
using SYF.Services;

namespace SYF.Infrastructure.Services
{
    public interface IDepartmentService : IService
    {
        Task<DepartmentModel> GetByIdAsync(Guid id);
        Task<IEnumerable<DepartmentModel>> SearchAsync(DepartmentSearchRequest request);
        Task<CreateResponse> CreateAsync(DepartmentModel model);
        Task UpdateAsync(Guid id, DepartmentModel model);
        Task DeleteAsync(Guid id);
    }
}

