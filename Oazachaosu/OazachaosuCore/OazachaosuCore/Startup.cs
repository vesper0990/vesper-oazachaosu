using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OazachaosuCore.Data;
using OazachaosuCore.Services;
using Repository;
using OazachaosuCore.Helpers;
using OazachaosuCore.Mappers;
using System;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using OazachaosuCore.Modules;
using OazachaosuCore.Settings;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.Extensions.Logging;
using NLog.Extensions.Logging;
using NLog.Web;

namespace OazachaosuCore
{
    public class Startup
    {

        public IContainer ApplicationContainer { get; private set; }

        public Startup(IHostingEnvironment currentEnviroment)
        {
            CurrentEnvironment = currentEnviroment;
            var builder = new ConfigurationBuilder()
                .SetBasePath(currentEnviroment.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfiguration Configuration { get; }
        public IHostingEnvironment CurrentEnvironment { get; }

        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            if (CurrentEnvironment.IsEnvironment("Testing"))
            {
                services.AddDbContext<ApplicationDbContext>(options =>
                options.UseMySql(@"Server=localhost;database=unittests;uid=root;pwd=Akuku123;"));
            }
            else
            {
                services.AddDbContext<ApplicationDbContext>(options =>
                options.UseMySql(@"Server=localhost;database=test;uid=root;pwd=Akuku123;"));
            }
            services.AddScoped<IDatabaseContext, ApplicationDbContext>();
            services.AddScoped<IEmailSender, EmailSender>();
            services.AddScoped<IBodyProvider, SimpleBodyProvider>();
            services.AddScoped<IHeaderElementProvider, HeaderElementProvider>();
            services.AddScoped<IWordkiRepo, WordkiRepo>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IDataInitializer, DataInitializer>();
            services.AddSingleton(AutoMapperConfig.Initialize());

            services.AddMvc();

            var builder = new ContainerBuilder();
            builder.Populate(services);
            builder.RegisterModule(new SettingsModule(Configuration));
            ApplicationContainer = builder.Build();

            return new AutofacServiceProvider(ApplicationContainer);
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env,
            IApplicationLifetime appLifeTime, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddNLog();
            env.ConfigureNLog("nlog.config");

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            var generalSettings = app.ApplicationServices.GetService<GeneralSettings>();
            if (generalSettings.SeedData)
            {
                var dataInitializer = app.ApplicationServices.GetService<IDataInitializer>();
                dataInitializer.SeedAsync();
            }
            app.UseMiddleware(typeof(Framework.ExceptionHandlerMiddleware));
            app.UseStaticFiles();
            app.UseAuthentication();
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });

            appLifeTime.ApplicationStopped.Register(() => ApplicationContainer.Dispose());
        }
    }
}
