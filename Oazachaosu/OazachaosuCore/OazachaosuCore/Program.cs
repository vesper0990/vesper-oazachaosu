using System.Linq;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using OazachaosuCore.Data;

namespace OazachaosuCore
{
    public class Program
    {
        public static void Main(string[] args)
        {

            using(var context = new ApplicationDbContext())
            {
                context.Database.EnsureCreated();

                if(context.Groups.Count() == 0)
                {
                    DatabaseSeed.Up();
                }

            }
            BuildWebHost(args).Run();
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .Build();
    }
}
