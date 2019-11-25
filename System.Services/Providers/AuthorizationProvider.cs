using Newtonsoft.Json;
using SYF.Data;
using SYF.Infrastructure.Providers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace SYF.Services.Providers
{
    public class AuthorizationProvider : IAuthorizationProvider
    {
        public AuthorizationProvider(DataContext context)
        {
            DataContext = context;
        }

        private DataContext DataContext { get; }
        private string[] Permissions { get; set; }

        public async Task InitializeAsync(ClaimsPrincipal principal)
        {
            IEnumerable<string> permissions = new List<string>();

            //var roleList = (await Cache.GetRolesAsync(DataContext)).ToList();
            //foreach (var roleId in principal.GetRoles())
            //{
            //    var role = roleList.FirstOrDefault(x => x.Id == roleId);
            //    if (role == null) continue;

            //    var rolePermissions = JsonConvert.DeserializeObject<string[]>(role.Permissions);
            //    permissions = permissions.Union(rolePermissions);
            //}

            Permissions = permissions.ToArray();
        }

        public bool HasPermission(string permission)
        {
            if (Permissions == null || Permissions.Length == 0) return false;

            return permission.EndsWith("*") ? CheckPermissionStartsWith(permission) : CheckPermissionEquals(permission);
        }

        private bool CheckPermissionStartsWith(string permission)
        {
            permission = permission.TrimEnd('*');

            return Permissions.Any(x => x.StartsWith(permission, StringComparison.OrdinalIgnoreCase));
        }

        private bool CheckPermissionEquals(string permission)
        {
            return Permissions.Any(x => x.Equals(permission, StringComparison.OrdinalIgnoreCase));
        }
    }
}
