using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SYF.Infrastructure.Models;
using SYF.Infrastructure.Models.Requests;
using SYF.Infrastructure.Models.Responses;
using SYF.Infrastructure.Services;

namespace SYF.Web.Controllers
{
    [Route("api/departments")]
    public class DepartmentController : Controller
    {
        public DepartmentController(IDepartmentService departmentService)
        {
            DepartmentService = departmentService;
        }

        public IDepartmentService DepartmentService { get; }

        [HttpGet("{id}")]
        public Task<DepartmentModel> GetByIdAsync(Guid id)
        {
            return DepartmentService.GetByIdAsync(id);
        }

        [HttpGet()]
        public Task<IEnumerable<DepartmentModel>> SearchAsync([FromQuery]DepartmentSearchRequest request)
        {
            return DepartmentService.SearchAsync(request);
        }

        [HttpPost]
        public Task<CreateResponse> CreateAsync([FromBody] DepartmentModel model)
        {
            return DepartmentService.CreateAsync(model);
        }

        [HttpPut("{id}")]
        public Task UpdateAsync(Guid id, [FromBody] DepartmentModel model)
        {
            return DepartmentService.UpdateAsync(id, model);
        }

        [HttpDelete("{id}")]
        public Task DeleteAsync(Guid id)
        {
            return DepartmentService.DeleteAsync(id);
        }
    }
}

