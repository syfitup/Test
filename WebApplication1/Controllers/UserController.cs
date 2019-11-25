using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SYF.Infrastructure.Models;
using SYF.Infrastructure.Models.Requests;
using SYF.Infrastructure.Models.Responses;
using SYF.Services;
using System.Threading.Tasks; 

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    public class UserController : Controller
    {
        public UserController(IUserService userService)
        {
            UserService = userService;
        }

        public IUserService UserService { get; }

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
