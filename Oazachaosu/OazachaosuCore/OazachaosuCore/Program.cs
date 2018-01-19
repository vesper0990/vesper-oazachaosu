using System.Linq;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using OazachaosuCore.Data;
using Repository;

namespace OazachaosuCore
{
    public class Program
    {
        public static void Main(string[] args)
        {

            using (var context = new ApplicationDbContext(ApplicationDbContext.GetOptions()))
            {
                context.Database.EnsureCreated();

                if (context.Groups.Count() == 0)
                {
                    DatabaseSeed.Up();
                }
                //Group group = context.Groups.FirstOrDefault();
                //Word word = new Word()
                //{
                //    Id = 100,
                //};
                //group.AddWord(word);
                //context.Words.Add(word);
                //context.SaveChanges();

            }
            BuildWebHost(args).Run();
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .UseUrls("http://localhost:5001/")
                .Build();
    }
}
