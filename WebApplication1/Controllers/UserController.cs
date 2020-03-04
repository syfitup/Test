using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SYF.Infrastructure.Models;
using SYF.Infrastructure.Models.Requests;
using SYF.Infrastructure.Models.Responses;
using SYF.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks; 

namespace SYF.Web.Controllers
{
    [Route("api/users")]
    public class UserController : Controller
    {
        public UserController(IUserService userService)
        {
            UserService = userService;
        }

        public IUserService UserService { get; }

        [HttpGet("{id}")]
        public Task<UserModel> GetByIdAsync(Guid id)
        {
            return UserService.GetByIdAsync(id);
        }

        [HttpGet()]
        public Task<List<UserSummary>> SearchAsync([FromQuery]UserSearchRequest request)
        {
            return UserService.SearchAsync(request);
        }

        [AllowAnonymous]
        [HttpPost("authenticate")]
        public Task<AuthenticateResponse> AuthenticateAsync([FromBody]AuthenticateRequest request)
        {
            return UserService.AuthenticateAsync(request);
        }

        [HttpPost]
        [HttpPost("register")]
        //[Permission("admin.users")]
        public Task<CreateResponse> RegisterAsync([FromBody]UserModel model)
        {
            return UserService.CreateAsync(model);
        }
    }
}
