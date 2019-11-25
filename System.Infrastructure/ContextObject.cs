using Microsoft.Extensions.Options;
using SYF.Framework;
using SYF.Infrastructure.Providers;
using System;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;

namespace SYF.Infrastructure
{
    public abstract class ContextObject : DisposableObject
    {
        private AsyncLazy<IAuthorizationProvider> _authorizationProvider;

        protected ContextObject(IServiceProvider serviceProvider, IClaimsPrincipalProvider principalProvider, ILoggerFactory loggerFactory)
        {
            ServiceProvider = serviceProvider;
            ClaimsPrincipalProvider = principalProvider;
            Logger = loggerFactory.CreateLogger(GetType());

            InitAuthorisationProvider();
        }

        public IServiceProvider ServiceProvider { get; private set; }

        public ILogger Logger { get; }
        public IClaimsPrincipalProvider ClaimsPrincipalProvider { get; }
        public ClaimsPrincipal ClaimsPrincipal
        {
            get => ClaimsPrincipalProvider.GetPrincipal();
            set
            {
                ClaimsPrincipalProvider.SetPrincipal(value);

                if (_authorizationProvider.IsValueCreated)
                {
                    InitAuthorisationProvider();
                }
            }
        }

        protected TService GetService<TService>() where TService : class
        {
            return ServiceProvider.GetService<TService>();
        }

        protected TOptions GetOptions<TOptions>() where TOptions : class, new()
        {
            var options = ServiceProvider.GetService<IOptions<TOptions>>();

            return options.Value;
        }

        public async Task<bool> HasPermission(string permission)
        {
            var provider = await _authorizationProvider;

            return provider.HasPermission(permission);
        }

        /// <summary>
        /// Create an asynchronous lazy loader for the authorisation provider
        /// </summary>
        private void InitAuthorisationProvider()
        {
            _authorizationProvider = new AsyncLazy<IAuthorizationProvider>(async () =>
            {
                var provider = ServiceProvider.GetService<IAuthorizationProvider>();

                await provider.InitializeAsync(ClaimsPrincipal);

                return provider;
            });
        }

        protected override void DisposeManaged()
        {
            _authorizationProvider = null;
            ServiceProvider = null;

            base.DisposeManaged();
        }
    }
}

