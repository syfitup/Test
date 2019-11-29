using Autofac;
using Autofac.Extensions.DependencyInjection;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.SpaServices.ReactDevelopmentServer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Swashbuckle.AspNetCore.Swagger;
using SYF.Data;
using SYF.Infrastructure;
using SYF.Infrastructure.Configuration;
using SYF.Infrastructure.Providers;
using SYF.Services;
using SYF.Services.Providers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using WebApplication1.Filters;
using WebApplication1.Providers;

namespace WebApplication1
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.Configure<SystemOptions>(Configuration.GetSection("System"));

            services.AddControllersWithViews();

            // Inject an implementation of ISwaggerProvider with defaulted settings applied
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new Info { Title = "PIPware API", Version = "v1" });
                options.OperationFilter<SwaggerOperationNameFilter>();

                options.DescribeAllEnumsAsStrings();
                options.UseReferencedDefinitionsForEnums();
            });

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddSingleton<IClaimsPrincipalProvider, ClaimsPrincipalProvider>();

            services.AddTransient<IUserService, UserService>();
            services.AddTransient<ISecurityProvider, SecurityProvider>();
            services.AddTransient<IClaimsPrincipalProvider, ClaimsPrincipalProvider>();


            // In production, the React files will be served from this directory
            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "ClientApp/build";
            });

            // Get the modules to load
            var modules = new List<ModuleOptions>();
            Configuration.GetSection("Modules").Bind(modules);

            ConfigureDbContexts(services);

            // Auto Mapper Configurations
            //var mappingConfig = new MapperConfiguration(mc =>
            //{
            //    mc.AddProfile(new MappingProfile());
            //});

            services.AddAutoMapper(typeof(MappingProfile));

            //IMapper mapper = mappingConfig.CreateMapper();
            //services.AddSingleton(mapper);

            // Use AutoFac for dependency injection
            var container = ConfigureContainer(services);

            return new AutofacServiceProvider(container);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();


            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller}/{action=Index}/{id?}");
            });

            app.UseSpa(spa =>
            {
                spa.Options.SourcePath = "ClientApp";

                if (env.IsDevelopment())
                {
                    spa.UseReactDevelopmentServer(npmScript: "start");
                }
            });
        }

        private IContainer ConfigureContainer(IServiceCollection services)
        {
            // Load the modules defined in config
            var containerBuilder = new ContainerBuilder();
            var loader = new ModuleLoader(containerBuilder);

            containerBuilder.Populate(services);

            return containerBuilder.Build();
        }

        private void ConfigureDbContexts(IServiceCollection services)
        {
            // Common DbContext instances... 
            services.AddDbContext<DataContext>();
            services.AddDbContext<TransientDataContext>(ServiceLifetime.Transient);
        }

    }
}
