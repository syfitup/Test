using Microsoft.Extensions.Logging;
using SYF.Infrastructure;
using SYF.Infrastructure.Providers;
using SYF.Data;
using System;


namespace SYF.Services.Services
{
    public abstract class BaseService : ContextObject, IService
    {
        protected BaseService(DataContext context, IServiceProvider serviceProvider, IClaimsPrincipalProvider principalProvider, ILoggerFactory loggerFactory)
            : base(serviceProvider, principalProvider, loggerFactory)
        {
            DataContext = context;
        }

        public DataContext DataContext { get; private set; }
    }
}
