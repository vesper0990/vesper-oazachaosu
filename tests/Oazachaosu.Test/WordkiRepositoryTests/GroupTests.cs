using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using Oazachaosu.Api.Data;
using Oazachaosu.Core;
using Oazachaosu.Core.Common;
using System;
using System.Linq;

namespace OazachaosuCore.Test.WordkiRepositoryTests
{
    [TestFixture]
    public class GroupTests
    {

        DbContextOptions<ApplicationDbContext> Options { get; set; }

        public GroupTests()
        {

        }

        [SetUp]
        public void SetUp()
        {
            Options = DatabaseUtil.GetOptions();
            DatabaseUtil.ClearDatabase(Options);
            DatabaseUtil.SetUser(Options);
        }

        /// <summary>
        /// Try to add default group to database. Check groups count and equivalation groups from database and 
        /// this one which was added.
        /// </summary>
        [Test]
        public void Add_group_test()
        {
            Group groupToAdd = GetGroup();
            using (var context = new ApplicationDbContext(Options))
            {
                IWordkiRepo repository = new WordkiRepo(context);
                repository.AddGroup(groupToAdd);
                repository.SaveChanges();
            }

            using (var context = new ApplicationDbContext(Options))
            {
                Assert.AreEqual(1, context.Groups.Count());
                Group groupFromDatabase = context.Groups.Single();
                CheckGroups(groupToAdd, groupFromDatabase);
            }
        }

        /// <summary>
        /// Add group to empty database with Id set on 0. Check groups count and
        /// if added group in database has Id not equal 0 (it should be increased).
        /// </summary>
        [Test]
        public void Add_group_without_id_test()
        {
            Group groupToAdd = GetGroup();
            groupToAdd.Id = 0;
            using (var context = new ApplicationDbContext(Options))
            {
                IWordkiRepo repository = new WordkiRepo(context);
                repository.AddGroup(groupToAdd);
                repository.SaveChanges();
            }
            using (var context = new ApplicationDbContext(Options))
            {
                Assert.AreEqual(1, context.Groups.Count());
                Group groupFromDatabase = context.Groups.Single();
                Assert.AreNotEqual(0, groupFromDatabase.Id);
                groupToAdd.Id = 1;
                CheckGroups(groupToAdd, groupFromDatabase);
            }
        }

        /// <summary>
        /// Add couple of groups with Id set on 0. Check groups count and 
        /// if all groups have Id different then 0.
        /// </summary>
        [Test]
        public void Add_group_without_id_with_another_groups()
        {
            for (int i = 1; i < 10; i++)
            {
                Group groupToAdd = new Group()
                {
                    Id = 0,
                    Language1 = LanguageType.English,
                    Language2 = LanguageType.Italian,
                    CreationDate = DateTime.Now,
                    LastChange = DateTime.Now,
                    Name = $"name {i}",
                    State = 1,
                    UserId = DatabaseUtil.User.Id,
                };
                using (var context = new ApplicationDbContext(Options))
                {
                    IWordkiRepo repository = new WordkiRepo(context);
                    repository.AddGroup(groupToAdd);
                    repository.SaveChanges();
                }
            }
            using (var context = new ApplicationDbContext(Options))
            {
                Assert.AreEqual(9, context.Groups.Count());
                foreach(Group group in context.Groups)
                {
                    Assert.AreNotEqual(0, group.Id);
                }
            }
        }
        
        /// <summary>
        /// Add group to database with two users and group with the same Id.
        /// Check groups count.
        /// </summary>
        [Test]
        public void Add_group_with_the_same_id_and_different_userId_test()
        {
            Group groupToAdd = GetGroup();
            User secondUser = new User()
            {
                Id = 2,
                Name = "asdf",
                Password = "asdf",
                ApiKey = "asdf"
            };
            using (var context = new ApplicationDbContext(Options))
            {
                context.Groups.Add(groupToAdd);
                context.Users.Add(secondUser);
                context.SaveChanges();
            }

            using (var context = new ApplicationDbContext(Options))
            {
                IWordkiRepo repository = new WordkiRepo(context);
                groupToAdd.UserId = secondUser.Id;
                repository.AddGroup(groupToAdd);
                repository.SaveChanges();
            }

            using (var context = new ApplicationDbContext(Options))
            {
                Assert.AreEqual(2, context.Groups.Count());
            }
        }

        /// <summary>
        /// Try to add same group twice. Check if execption was thrown.
        /// </summary>
        [Test]
        public void Try_to_add_same_group_test()
        {
            Group groupToAdd = GetGroup();
            using (var context = new ApplicationDbContext(Options))
            {
                context.Groups.Add(groupToAdd);
                context.SaveChanges();
            }

            using (var context = new ApplicationDbContext(Options))
            {
                IWordkiRepo repository = new WordkiRepo(context);
                repository.AddGroup(groupToAdd);
                Assert.Throws<DbUpdateException>(new TestDelegate(repository.SaveChanges));
            }
        }

        /// <summary>
        /// Try to update group. Check groups count and equivalence of updated group.
        /// </summary>
        [Test]
        public void Update_group_test()
        {
            Group groupToAdd = GetGroup();
            using (var context = new ApplicationDbContext(Options))
            {
                context.Groups.Add(groupToAdd);
                context.SaveChanges();
            }

            Group groupToUpdate = new Group()
            {
                Id = 2,
                Language1 = LanguageType.Germany,
                Language2 = LanguageType.Portuaglese,
                CreationDate = new DateTime(1990, 1, 1),
                LastChange = new DateTime(1990, 1, 1),
                Name = "nameafds",
                State = 112,
                UserId = DatabaseUtil.User.Id,
            };

            using (var context = new ApplicationDbContext(Options))
            {
                IWordkiRepo repository = new WordkiRepo(context);
                repository.UpdateGroup(groupToUpdate);
                repository.SaveChanges();
            }

            using (var context = new ApplicationDbContext(Options))
            {
                Assert.AreEqual(1, context.Groups.Count());
                CheckGroups(groupToUpdate, context.Groups.Single());
            }
        }

        /// <summary>
        /// Try to get groups from database. Check group count
        /// </summary>
        [Test]
        public void Get_group_test()
        {
            Group groupToAdd = GetGroup();
            using (var context = new ApplicationDbContext(Options))
            {
                context.Groups.Add(groupToAdd);
                context.SaveChanges();
            }

            using (var context = new ApplicationDbContext(Options))
            {
                IWordkiRepo repository = new WordkiRepo(context);
                Group groupFromDatabase = repository.GetGroups().Single();
                CheckGroups(groupToAdd, groupFromDatabase);
            }
        }

        private void CheckGroups(Group expexted, Group actual)
        {
            Assert.AreEqual(expexted.Id, actual.Id);
            Assert.AreEqual(expexted.UserId, actual.UserId);
            Assert.AreEqual(expexted.Language1, actual.Language1);
            Assert.AreEqual(expexted.Language2, actual.Language2);
            Assert.AreEqual(expexted.CreationDate, actual.CreationDate);
            Assert.AreEqual(expexted.LastChange, actual.LastChange);
            Assert.AreEqual(expexted.Name, actual.Name);
            Assert.AreEqual(expexted.State, actual.State);
        }

        private Group GetGroup()
        {
            return new Group()
            {
                Id = 2,
                Language1 = LanguageType.English,
                Language2 = LanguageType.Italian,
                CreationDate = new DateTime(1990, 1, 1),
                LastChange = new DateTime(1990, 1, 1),
                Name = "name",
                State = 1,
                UserId = DatabaseUtil.User.Id,
            };
        }
    }
}
