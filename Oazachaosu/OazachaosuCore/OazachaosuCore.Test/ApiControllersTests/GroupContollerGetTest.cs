using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using OazachaosuCore.Controllers;
using OazachaosuCore.Helpers;
using OazachaosuCore.Helpers.Respone;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OazachaosuCore.Test.ApiControllersTests
{
    [TestFixture]
    public class GroupContollerGetTest
    {

        GroupsController controller;
        Mock<IHeaderElementProvider> headerElementProviderMock = new Mock<IHeaderElementProvider>();

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
            controller = new GroupsController(DatabaseUtil.GetWordkiRepoWithDate());
        }

        [Test]
        public void Check_group_count()
        {
            JsonResult result = controller.Get(headerElementProviderMock.Object) as JsonResult;
            Assert.NotNull(result);

            ApiResult apiResult = result.Value as ApiResult;
            Assert.NotNull(apiResult);

            Assert.AreEqual(ResultCode.Done, apiResult.Code);

            IEnumerable<Group> groups = apiResult.Object as IEnumerable<Group>;
            Assert.AreEqual(Utility.GroupCount, groups.Count());
        }

        [Test]
        public void Check_group_count_by_user()
        {
            User user = new User()
            {
                Id = 2,
                ApiKey = "key",
                Name = "name2",
                Password = "pass"
            };
            DatabaseUtil.Context.Users.Add(user);
            Group group = new Group()
            {
                UserId = 2,
                Name = "groupa",
                Id = 999,
                CreationDate = new DateTime(2018, 1, 1),
            };
            DatabaseUtil.Context.Groups.Add(group);
            DatabaseUtil.Context.SaveChanges();

            JsonResult result = controller.Get(headerElementProviderMock.Object) as JsonResult;
            Assert.NotNull(result);

            ApiResult apiResult = result.Value as ApiResult;
            Assert.NotNull(apiResult);

            Assert.AreEqual(ResultCode.Done, apiResult.Code);

            IEnumerable<Group> groups = apiResult.Object as IEnumerable<Group>;
            Assert.AreEqual(Utility.GroupCount, groups.Count());
        }

        [Test]
        public void Check_group_count_by_last_change()
        {
            Group group = new Group()
            {
                UserId = 1,
                Name = "groupa",
                Id = 999,
                LastChange = new DateTime(1999, 1, 1),
            };
            DatabaseUtil.Context.Groups.Add(group);
            DatabaseUtil.Context.SaveChanges();

            JsonResult result = controller.Get(headerElementProviderMock.Object) as JsonResult;
            Assert.NotNull(result);

            ApiResult apiResult = result.Value as ApiResult;
            Assert.NotNull(apiResult);

            Assert.AreEqual(ResultCode.Done, apiResult.Code);

            IEnumerable<Group> groups = apiResult.Object as IEnumerable<Group>;
            Assert.AreEqual(Utility.GroupCount, groups.Count());
        }

        [Test]
        public void Check_words_count()
        {
            JsonResult result = controller.Get(headerElementProviderMock.Object) as JsonResult;
            Assert.NotNull(result);

            ApiResult apiResult = result.Value as ApiResult;
            Assert.NotNull(apiResult);

            Assert.AreEqual(ResultCode.Done, apiResult.Code);

            IEnumerable<Group> groups = apiResult.Object as IEnumerable<Group>;
            foreach (Group group in groups)
            {
                Assert.AreEqual(Utility.WordCount, group.Words.Count);
            }
        }

        [Test]
        public void Check_result_count()
        {
            JsonResult result = controller.Get(headerElementProviderMock.Object) as JsonResult;
            Assert.NotNull(result);

            ApiResult apiResult = result.Value as ApiResult;
            Assert.NotNull(apiResult);

            Assert.AreEqual(ResultCode.Done, apiResult.Code);

            IEnumerable<Group> groups = apiResult.Object as IEnumerable<Group>;

            foreach (Group group in groups)
            {
                Assert.AreEqual(Utility.ResultCount, group.Results.Count);
            }
        }

    }
}
