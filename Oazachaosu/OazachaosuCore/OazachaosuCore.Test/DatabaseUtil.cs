using Microsoft.EntityFrameworkCore;
using OazachaosuCore.Data;
using Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace OazachaosuCore.Test
{
    public static class DatabaseUtil
    {

        public static IServiceProvider ServiceProvider { get; set; }

        static DatabaseUtil()
        {
            //ServiceProvider = new 
        }

        public static ApplicationDbContext GetEmptyDbContext()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                      .UseInMemoryDatabase(Guid.NewGuid().ToString())
                      .Options;
            var context = new ApplicationDbContext(options);
            return context;
        }

        public static ApplicationDbContext GetDbContextWithData()
        {
            ApplicationDbContext context = GetEmptyDbContext();

            context.Groups.Add(new Group() { Id = 1, Name = "jaks" });

            return context;
        }

        public static IWordkiRepo GetEmptyWordkiRepo()
        {
            IWordkiRepo repo = new WordkiRepo(GetEmptyDbContext());
            return repo;
        }

        public static IWordkiRepo GetWordkiRepoWithDate()
        {
            IWordkiRepo repo = new WordkiRepo(GetDbContextWithData());
            return repo;
        }

    }
}
