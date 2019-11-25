using System.Security.Claims;

namespace SYF.Infrastructure.Providers
{
    public interface IClaimsPrincipalProvider
    {
        ClaimsPrincipal GetPrincipal();
        void SetPrincipal(ClaimsPrincipal principal);
    }
}
