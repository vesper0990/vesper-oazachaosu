using Microsoft.EntityFrameworkCore;
using OazachaosuCore.Data;
using Repository;
using System;

namespace OazachaosuCore.Test
{
    public static class DatabaseUtil
    {

        static DatabaseUtil()
        {
            User = new User()
            {
                Id = 1,
                ApiKey = "apikey",
                Name = "user",
                Password = "password",
            };
        }

        public static User User { get; set; }

        public static ApplicationDbContext Context { get; set; }


        public static ApplicationDbContext GetEmptyDbContext()
        {
            ApplicationDbContext.test = true;
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                      .UseInMemoryDatabase("test")
                      .Options;
            Context = new ApplicationDbContext(options);

            Context.Groups.Add(new Group() { Id = 1 });
            Context.SaveChanges();
            Context.Groups.Update(new Group() { Id = 1 });
            Context.SaveChanges();
            return Context;
        }



        public static ApplicationDbContext GetDbContextWithData()
        {
            ApplicationDbContext context = GetEmptyDbContext();

            context.Groups.AddRange(Utility.GetGroups());
            context.Users.Add(User);
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
