using Microsoft.EntityFrameworkCore;
using OazachaosuCore.Data;
using Repository;
using System.Collections.Generic;

namespace OazachaosuCore.Test
{
    public static class DatabaseUtil
    {

        static DatabaseUtil()
        {
            ApplicationDbContext.test = true;
            User = new User()
            {
                Id = 1,
                ApiKey = "apikey",
                Name = "user",
                Password = "password",
            };
        }

        public static User User { get; set; }

        public static DbContextOptions<ApplicationDbContext> GetOptions()
        {
            return new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseMySql(@"Server=localhost;database=unittests;uid=root;pwd=Akuku123;")
                .Options;
        }

        public static void SetUser(DbContextOptions<ApplicationDbContext> options)
        {
            using (var context = new ApplicationDbContext(options))
            {
                context.Users.Add(User);
                context.SaveChanges();
            }
        }

        public static void SetData(DbContextOptions<ApplicationDbContext> options)
        {
            SetUser(options);
            using (var context = new ApplicationDbContext(options))
            {
                IEnumerable<Group> groups = Utility.GetGroups();
                foreach (Group group in groups)
                {
                    group.UserId = User.Id;
                    group.LastChange = new System.DateTime(2018, 1, 1);
                    foreach (Word word in group.Words)
                    {
                        word.LastChange = new System.DateTime(2018, 1, 1);
                    }
                    foreach (Result result in group.Results)
                    {
                        result.LastChange = new System.DateTime(2018, 1, 1);
                    }
                }
                context.Groups.AddRange(groups);
                context.SaveChanges();
            }
        }

        public static void ClearDatabase(DbContextOptions<ApplicationDbContext> options)
        {
            using (var context = new ApplicationDbContext(options))
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();
            }
        }

    }
}
