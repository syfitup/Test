using SYF.Infrastructure.Models;
using SYF.Infrastructure.Models.Requests;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace SYF.Infrastructure.Providers
{
    public interface ISecurityProvider
    {
        Task<ClaimsPrincipal> CreatePrincipalAsync(Guid userId);
        ClaimsPrincipal CreateSystemPrincipal(Guid siteId);
        string CreateToken(SecurityTokenRequest request);
        string UserApiToString(UserApiModel model);
        UserApiModel UserApiFromString(string value);
        bool CheckSuperUserPassword(string password);

        string EncryptValue(string value, string iv);
        string DecryptValue(string base64Value, string iv);
    }
}
