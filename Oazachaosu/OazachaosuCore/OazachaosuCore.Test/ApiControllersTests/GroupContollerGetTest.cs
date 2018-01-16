using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;
using OazachaosuCore.Controllers;
using OazachaosuCore.Data;
using OazachaosuCore.Helpers;
using OazachaosuCore.Helpers.Respone;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using WordkiModelCore.DTO;

namespace OazachaosuCore.Test.ApiControllersTests
{
    [TestFixture]
    public class GroupContollerGetTest
    {

        Mock<IHeaderElementProvider> headerElementProviderMock = new Mock<IHeaderElementProvider>();
        DbContextOptions<ApplicationDbContext> Options { get; set; }

        public GroupContollerGetTest()
        {
            Utility.GroupCount = 2;
            Utility.WordCount = 2;
            Utility.ResultCount = 2;

            headerElementProviderMock.Setup(x => x.GetElement(It.IsAny<HttpRequest>(), "dateTime")).Returns("2000/01/01");
            headerElementProviderMock.Setup(x => x.GetElement(It.IsAny<HttpRequest>(), "apikey")).Returns(DatabaseUtil.User.ApiKey);
        }

        [SetUp]
        public void SetUp()
        {
            Options = DatabaseUtil.GetOptions();
            DatabaseUtil.ClearDatabase(Options);
            DatabaseUtil.SetData(Options);
        }

        /// <summary>
        /// Check if all groups with proper UserId and LastChange date is given by Get method
        /// </summary>
        [Test]
        public void Check_group_count()
        {
            JsonResult result;
            using (var context = new ApplicationDbContext(Options))
            {
                result = new GroupsController(new WordkiRepo(context)).Get(headerElementProviderMock.Object) as JsonResult;
                Assert.NotNull(result);

                ApiResult apiResult = result.Value as ApiResult;
                Assert.NotNull(apiResult);

                Assert.AreEqual(ResultCode.Done, apiResult.Code);

                IEnumerable<GroupDTO> groups = apiResult.Object as IEnumerable<GroupDTO>;
                Assert.AreEqual(Utility.GroupCount, groups.Count());
            }
        }

        /// <summary>
        /// Check if groups own to another user is not passed
        /// </summary>
        [Test]
        public void Check_group_count_by_user()
        {
            using(var context = new ApplicationDbContext(Options))
            {
                User user = new User()
                {
                    Id = 2,
                    ApiKey = "key",
                    Name = "name2",
                    Password = "pass"
                };
                context.Users.Add(user);
                Group group = new Group()
                {
                    UserId = 2,
                    Name = "groupa",
                    Id = 999,
                    CreationDate = new DateTime(2018, 1, 1),
                };
                context.Groups.Add(group);
                context.SaveChanges();
            }
            using(var context = new ApplicationDbContext(Options))
            {
                JsonResult result = new GroupsController(new WordkiRepo(context)).Get(headerElementProviderMock.Object) as JsonResult;
                Assert.NotNull(result);

                ApiResult apiResult = result.Value as ApiResult;
                Assert.NotNull(apiResult);

                Assert.AreEqual(ResultCode.Done, apiResult.Code);

                IEnumerable<GroupDTO> groups = apiResult.Object as IEnumerable<GroupDTO>;
                Assert.AreEqual(Utility.GroupCount, groups.Count());
            }
        }

        /// <summary>
        /// Check if groups with out update is not passed
        /// </summary>
        [Test]
        public void Check_group_count_by_last_change()
        {
            using (var context = new ApplicationDbContext(Options))
            {
                Group group = new Group()
                {
                    UserId = 1,
                    Name = "groupa",
                    Id = 999,
                    LastChange = new DateTime(1999, 1, 1),
                };
                context.Groups.Add(group);
                context.SaveChanges();
            }
            using (var context = new ApplicationDbContext(Options))
            {
                JsonResult result = new GroupsController(new WordkiRepo(context)).Get(headerElementProviderMock.Object) as JsonResult;
                Assert.NotNull(result);

                ApiResult apiResult = result.Value as ApiResult;
                Assert.NotNull(apiResult);

                Assert.AreEqual(ResultCode.Done, apiResult.Code);

                IEnumerable<GroupDTO> groups = apiResult.Object as IEnumerable<GroupDTO>;
                Assert.AreEqual(Utility.GroupCount, groups.Count());
            }
        }
    }
}
