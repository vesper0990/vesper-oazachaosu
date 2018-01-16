using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using OazachaosuCore.Data;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OazachaosuCore.Test.WordkiRepositoryTests
{
    [TestFixture]
    public class UsersTests
    {

        DbContextOptions<ApplicationDbContext> Options { get; set; }

        public UsersTests()
        {
        }

        [SetUp]
        public void SetUp()
        {
            Options = DatabaseUtil.GetOptions();
            DatabaseUtil.ClearDatabase(Options);
        }

        [Test]
        public void Add_user_test()
        {
            User userToAdd = new User()
            {
                Name = "Name",
                Password = "Password",
                ApiKey = "ApiKey",
            };
            using (var context = new ApplicationDbContext(Options))
            {
                IWordkiRepo repository = new WordkiRepo(context);
                repository.AddUser(userToAdd);
                repository.SaveChanges();
            }
            using (var context = new ApplicationDbContext(Options))
            {
                User userFromDatabase = context.Users.Single();
                Assert.AreEqual(userToAdd.Name, userFromDatabase.Name);
                Assert.AreEqual(userToAdd.Password, userFromDatabase.Password);
                Assert.AreEqual(userToAdd.ApiKey, userFromDatabase.ApiKey);
                Assert.IsTrue(userFromDatabase.Id > 0);
            }
        }

        [Test]
        public void Add_some_user_test()
        {
            for (int i = 0; i < 10; i++)
            {
                User userToAdd = new User()
                {
                    Name = $"Name {i}",
                    Password = $"Password {i}",
                    ApiKey = "ApiKey",
                };
                using (var context = new ApplicationDbContext(Options))
                {
                    IWordkiRepo repository = new WordkiRepo(context);
                    repository.AddUser(userToAdd);
                    repository.SaveChanges();
                }
            }
            using (var context = new ApplicationDbContext(Options))
            {
                Assert.AreEqual(10, context.Users.Count());
                for (int i = 0; i < 10; i++)
                {
                    User userFromDatabase = context.Users.ToList()[i];
                    Assert.AreEqual($"Name {i}", userFromDatabase.Name);
                    Assert.AreEqual($"Password {i}", userFromDatabase.Password);
                    Assert.AreEqual($"ApiKey", userFromDatabase.ApiKey);
                }
            }
        }

        [Test]
        public void Add_the_same_user()
        {
            DatabaseUtil.SetUser(Options);
            using (var context = new ApplicationDbContext(Options))
            {
                User userToAdd = DatabaseUtil.User;
                userToAdd.Id = 1;
                IWordkiRepo repository = new WordkiRepo(context);
                repository.AddUser(userToAdd);
                Assert.Throws<DbUpdateException>(new TestDelegate(repository.SaveChanges));
            }
        }

        [Test]
        public void Update_user()
        {
            DatabaseUtil.SetUser(Options);
            User userToUpdate = new User()
            {
                Id = DatabaseUtil.User.Id,
                Name = "asdf",
                Password = "asdf",
                ApiKey = "asdf"
            };
            using (var context = new ApplicationDbContext(Options))
            {
                IWordkiRepo repository = new WordkiRepo(context);
                repository.UpdateUser(userToUpdate);
                repository.SaveChanges();
            }
            using (var context = new ApplicationDbContext(Options))
            {
                User userFromDatabase = context.Users.Single();
                Assert.AreEqual(userToUpdate.Name, userFromDatabase.Name);
                Assert.AreEqual(userToUpdate.Password, userFromDatabase.Password);
                Assert.AreEqual(userToUpdate.ApiKey, userFromDatabase.ApiKey);
                Assert.AreEqual(userToUpdate.Id, userFromDatabase.Id);
            }
        }

        [Test]
        public void Get_user_test()
        {
            DatabaseUtil.SetUser(Options);
            using (var context = new ApplicationDbContext(Options))
            {
                IWordkiRepo repository = new WordkiRepo(context);
                IEnumerable<User> users = repository.GetUsers();
                Assert.AreEqual(1, users.Count());
                User user = users.Single();
                Assert.AreEqual(DatabaseUtil.User.Name, user.Name);
                Assert.AreEqual(DatabaseUtil.User.Password, user.Password);
                Assert.AreEqual(DatabaseUtil.User.ApiKey, user.ApiKey);
                Assert.AreEqual(DatabaseUtil.User.Id, user.Id);
            }
        }

    }
}
