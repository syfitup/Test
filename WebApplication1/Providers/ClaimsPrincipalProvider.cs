using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using SYF.Infrastructure.Providers;

namespace WebApplication1.Providers
{
    public class ClaimsPrincipalProvider : IClaimsPrincipalProvider
    {
        public ClaimsPrincipalProvider(IHttpContextAccessor httpContextAccesor)
        {
            HttpContext = httpContextAccesor.HttpContext;
        }

        public HttpContext HttpContext { get; }

        public ClaimsPrincipal GetPrincipal()
        {
            return HttpContext?.User;
        }

        public void SetPrincipal(ClaimsPrincipal principal)
        {
            HttpContext.User = principal;
        }
    }
}
