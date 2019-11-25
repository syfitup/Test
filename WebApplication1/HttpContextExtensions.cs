using Microsoft.AspNetCore.Http;
using SYF.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1
{
    public static class HttpContextExtensions
    {
        private const string TenantContextKey = "pipware.Tenant";

        public static void SetTenant(this HttpContext context, TenantModel tenant)
        {
            context.Items[TenantContextKey] = tenant;
        }

        public static TenantModel GetTenant(this HttpContext context)
        {
            if (context.Items.TryGetValue(TenantContextKey, out var tenant))
            {
                return tenant as TenantModel;
            }

            return null;
        }
    }
}
