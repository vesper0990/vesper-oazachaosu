using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Oazachaosu.Api.Data;
using Oazachaosu.Api.Services;
using Oazachaosu.Core;
using Oazachaosu.Api.Helpers;
using Oazachaosu.Api.Mappers;
using System;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Oazachaosu.Api.Modules;
using Oazachaosu.Api.Settings;
using Microsoft.Extensions.Logging;
using NLog.Extensions.Logging;
using NLog.Web;

namespace Oazachaosu.Api
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
                options.UseMySql(@"Server=wordkidb;database=test;uid=user_name_1;pwd=my-secret-pw;"));
            }
            else
            {
                services.AddDbContext<ApplicationDbContext>(options =>
                options.UseMySql(@"Server=wordkidb;database=test;uid=user_name_1;pwd=my-secret-pw;"));
            }
            services.AddScoped<IDatabaseContext, ApplicationDbContext>();
            services.AddScoped<IEmailSender, EmailSender>();
            services.AddScoped<IBodyProvider, SimpleBodyProvider>();
            services.AddScoped<IHeaderElementProvider, HeaderElementProvider>();
            services.AddScoped<IWordkiRepo, WordkiRepo>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IGroupService, GroupService>();
            services.AddScoped<IWordService, WordService>();
            services.AddScoped<IResultService, ResultService>();
            services.AddScoped<IDataInitializer, DataInitializer>();
            services.AddSingleton(AutoMapperConfig.Initialize());

            services.AddCors(options => options.AddPolicy("AllowAll", p => p.AllowAnyOrigin()
                                                             .AllowAnyMethod()
                                                              .AllowAnyHeader()));

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
            app.UseCors("AllowAll");
            var generalSettings = app.ApplicationServices.GetService<GeneralSettings>();
            var dataInitializer = app.ApplicationServices.GetService<IDataInitializer>();
            dataInitializer.SeedAsync(generalSettings.SeedData).ConfigureAwait(false);
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
