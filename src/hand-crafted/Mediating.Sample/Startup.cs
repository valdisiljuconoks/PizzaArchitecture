using System;
using Mediating.Sample.Infrastructure;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using StructureMap;

namespace Mediating.Sample
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", true, true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", true);

            if(env.IsDevelopment())
                builder.AddUserSecrets();

            builder.AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            return UseStructureMapContainer(services);
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            if(env.IsDevelopment())
                app.UseDeveloperExceptionPage();

            app.UseStaticFiles();

            app.UseMvc(routes =>
                       {
                           routes.MapRoute("default",
                                           "{controller=Home}/{action=Index}/{id?}");
                       });
        }

        private IServiceProvider UseStructureMapContainer(IServiceCollection services)
        {
            services.AddMvc()
                    .AddControllersAsServices()
                    .AddMvcOptions(config => { config.Filters.Add(typeof(ExceptionToJsonFilter)); })
                    .AddRazorOptions(opt =>
                                     {
                                         opt.ViewLocationFormats.Add("~/Features/{1}/{0}.cshtml");
                                         opt.ViewLocationFormats.Add("~/Features/Shared/{0}.cshtml");
                                     });

            var container = new Container();

            container.Populate(services);
            container.Configure(config =>
                                {
                                    config.Scan(scanner =>
                                                {
                                                    scanner.TheCallingAssembly();
                                                    scanner.WithDefaultConventions();
                                                    scanner.LookForRegistries();
                                                });
                                });

            return container.GetInstance<IServiceProvider>();
        }
    }
}
