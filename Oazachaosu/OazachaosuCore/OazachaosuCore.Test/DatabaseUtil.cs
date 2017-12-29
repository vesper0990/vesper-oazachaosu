using Microsoft.EntityFrameworkCore;
using OazachaosuCore.Data;
using Repository;
using System;

namespace OazachaosuCore.Test
{
    public static class DatabaseUtil
    {

        public static ApplicationDbContext GetEmptyDbContext()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                      .UseInMemoryDatabase(Guid.NewGuid().ToString())
                      .Options;
            ApplicationDbContext context = new ApplicationDbContext(options);
            return context;
        }

        public static ApplicationDbContext GetDbContextWithData()
        {
            ApplicationDbContext context = GetEmptyDbContext();

            context.Groups.AddRange(Utility.GetGroups());
            context.SaveChanges();
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
