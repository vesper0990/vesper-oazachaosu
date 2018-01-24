using Microsoft.EntityFrameworkCore;
using Oazachaosu.Api.Data;
using Oazachaosu.Core;
using Oazachaosu.Core.Common;
using System;
using System.Collections.Generic;

namespace OazachaosuCore.Test
{
    public static class DatabaseUtil
    {

        static DatabaseUtil()
        {
            User = GetUser();
        }

        public static User User { get; set; }

        public static DbContextOptions<ApplicationDbContext> GetOptions() =>
            new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseMySql(@"Server=localhost;database=unittests;uid=root;pwd=Akuku123;")
                .Options;


        public static void SetUser(DbContextOptions<ApplicationDbContext> options, User user)
        {
            using (var context = new ApplicationDbContext(options))
            {
                context.Users.Add(user);
                context.SaveChanges();
            }
        }

        public static void SetUser(DbContextOptions<ApplicationDbContext> options)
        {
            SetUser(options, GetUser());
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

        public static void SetGroup(DbContextOptions<ApplicationDbContext> options)
        {
            SetGroup(options, GetGroup());
        }

        public static void SetGroup(DbContextOptions<ApplicationDbContext> options, Group group)
        {
            using (var context = new ApplicationDbContext(options))
            {
                context.Groups.Add(group);
                context.SaveChanges();
            }
        }

        public static Group GetGroup(long id = 1,
            LanguageType language1 = LanguageType.English,
            LanguageType language2 = LanguageType.Polish,
            string name = "Name")
        {
            Group group = new Group()
            {
                Id = id,
                UserId = User.Id,
                Language1 = language1,
                Language2 = language2,
                Name = name,
                State = 3,
                LastChange = new DateTime(2000, 1, 1),
            };
            return group;
        }

        public static Word GetWord(
            long id = 0,
            string language1 = "lang1",
            string language2 = "lang2",
            string language1Comment = "lang1Comment",
            string language2Comment = "lang2Comment",
            byte drawer = 3,
            bool visible = true,
            bool checkedUnchecked = true)
        {
            return new Word()
            {
                Id = id,
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

        public static User GetUser(
            long id = 1,
            string apiKey = "apiKey",
            string name = "name",
            string password = "password")
        {
            return new User()
            {
                Id = id,
                ApiKey = apiKey,
                Name = name,
                Password = password,
            };
        }
    }
}
