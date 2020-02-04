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
using SYF.Infrastructure.Services;
using SYF.Services;
using SYF.Services.Providers;
using SYF.Services.Service;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using WebApplication1.Filters;
using WebApplication1.Providers;
using Microsoft.OpenApi.Models;

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

            // Register the Swagger generator, defining 1 or more Swagger documents
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });
            });

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddSingleton<IClaimsPrincipalProvider, ClaimsPrincipalProvider>();

            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IDepartmentService, DepartmentService>();
            services.AddTransient<ISubDepartmentService, SubDepartmentService>();
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

            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });


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
