using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OazachaosuCore.Data;
using OazachaosuCore.Services;
using Repository;
using OazachaosuCore.Helpers;

namespace OazachaosuCore
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IHostingEnvironment currentEnviroment)
        {
            Configuration = configuration;
            CurrentEnvironment = currentEnviroment;
        }

        public IConfiguration Configuration { get; }
        public IHostingEnvironment CurrentEnvironment { get; }

        public void ConfigureServices(IServiceCollection services)
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
            services.AddScoped<IEmailSender, EmailSender>();
            services.AddScoped<IBodyProvider, SimpleBodyProvider>();
            services.AddScoped<IHeaderElementProvider, HeaderElementProvider>();
            services.AddScoped<IWordkiRepo>(provider => new WordkiRepo(services.BuildServiceProvider().GetService<ApplicationDbContext>()));

            services.AddMvc();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
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

            app.UseStaticFiles();
            app.UseAuthentication();
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
