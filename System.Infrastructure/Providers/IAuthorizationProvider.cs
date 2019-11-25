using System.Security.Claims;
using System.Threading.Tasks;

namespace SYF.Infrastructure.Providers
{
    public interface IAuthorizationProvider
    {
        Task InitializeAsync(ClaimsPrincipal principal);
        bool HasPermission(string permission);
    }
}
