using Microsoft.EntityFrameworkCore;
using OazachaosuCore.Data;
using Repository;
using System;
using System.Collections.Generic;
using WordkiModel;

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

        public static DbContextOptions<ApplicationDbContext> GetOptions() =>
            new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseMySql(@"Server=localhost;database=unittests;uid=root;pwd=Akuku123;")
                .Options;

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

        public static Group GetGroup(LanguageType language1 = LanguageType.English, LanguageType language2 = LanguageType.Polish, string name = "Name")
        {
            Group group = new Group()
            {
                Id = 1,
                UserId = User.Id,
                Language1 = language1,
                Language2 = language2,
                Name = name,
                State = 3,
                LastChange = new DateTime(2000, 1, 1),
            };
            return group;
        }

        public static Word GetWord(string language1 = "lang1",
            string language2 = "lang2",
            string language1Comment = "lang1Comment",
            string language2Comment = "lang2Comment",
            byte drawer = 3,
            bool visible = true,
            bool checkedUnchecked = true)
        {
            return new Word()
            {
                GroupId = 1,
                UserId = User.Id,
                Language1 = language1,
                Language1Comment = language1Comment,
                Language2 = language2,
                Language2Comment = language2Comment,
                Drawer = drawer,
                State = 2,
                IsVisible = visible,
                IsSelected = checkedUnchecked,
                LastRepeating = new DateTime(2000, 1, 1),
                LastChange = new DateTime(2000, 1, 1),

            };
        }

    }
}
