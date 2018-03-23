using System;
using FluentValidation.AspNetCore;
using Mediating.Sample.Infrastructure;
using Mediating.Sample.Infrastructure.DependencyRegistries;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StructureMap;

namespace Mediating.Sample
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        private IConfiguration Configuration { get; }

        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddMvc()
                    .AddControllersAsServices()
                    .AddMvcOptions(config => { config.Filters.Add(typeof(ExceptionToJsonFilter)); })
                    .AddRazorOptions(opt =>
                                     {
                                         opt.ViewLocationFormats.Add("~/Features/{1}/{0}.cshtml");
                                         opt.ViewLocationFormats.Add("~/Features/Shared/{0}.cshtml");
                                     })
                    .AddFluentValidation(_ => _.RegisterValidatorsFromAssemblyContaining<Startup>());

            services.AddMediatR();

            return UseStructureMapContainer(services);
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if(env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseMvc(routes =>
                       {
                           routes.MapRoute("default",
                                           "{controller=UserManagement}/{action=List}/{id?}");
                       });
        }

        private IServiceProvider UseStructureMapContainer(IServiceCollection services)
        {
            var container = new Container();

            container.Populate(services);
            container.Configure(config =>
                                {
                                    config.Scan(scanner =>
                                                {
                                                    scanner.TheCallingAssembly();
                                                    scanner.WithDefaultConventions();
                                                });

                                    config.AddRegistry<MediatorRegistrations>();
                                    config.AddRegistry<HttpRegistries>();
                                });

            return container.GetInstance<IServiceProvider>();
        }
    }
}
