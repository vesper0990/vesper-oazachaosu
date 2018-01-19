using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using OazachaosuCore.Data;
using System;
using System.Net.Http;

namespace OazachaosuCore.Test.EndToEndTests.ApiTests
{
    public class ApiTestBase : IDisposable
    {
        protected readonly TestServer server;
        protected readonly HttpClient client;
        protected DbContextOptions<ApplicationDbContext> options;

        public ApiTestBase()
        {
            server = new TestServer(new WebHostBuilder()
                .UseEnvironment("Testing")
                .UseStartup<Startup>());
            client = server.CreateClient();
            options = DatabaseUtil.GetOptions();

            using (var context = new ApplicationDbContext(options))
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();
            }
        }

        public void Dispose()
        {

        }
    }
}
