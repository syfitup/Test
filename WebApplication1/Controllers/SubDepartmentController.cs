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
    [Route("api/[controller]")]
    public class SubDepartmentController : Controller
    {
        public SubDepartmentController(ISubDepartmentService subDepartmentService)
        {
            SubDepartmentService = subDepartmentService;
        }

        public ISubDepartmentService SubDepartmentService { get; }

        [HttpGet("{id}")]
        public Task<SubDepartmentModel> GetByIdAsync(Guid id)
        {
            return SubDepartmentService.GetByIdAsync(id);
        }

        [HttpGet("[action]")]
        public Task<IEnumerable<SubDepartmentModel>> SearchAsync([FromQuery]SubDepartmentSearchRequest request)
        {
            return SubDepartmentService.SearchAsync(request);
        }

        [HttpPost]
        [HttpPost("register")]
        public Task<CreateResponse> CreateAsync([FromBody] SubDepartmentModel model)
        {
            return SubDepartmentService.CreateAsync(model);
        }

        [HttpPut("{id}")]
        public Task UpdateAsync(Guid id, [FromBody] SubDepartmentModel model)
        {
            return SubDepartmentService.UpdateAsync(id, model);
        }

        [HttpDelete("{id}")]
        public Task DeleteAsync(Guid id)
        {
            return SubDepartmentService.DeleteAsync(id);
        }
    }
}

