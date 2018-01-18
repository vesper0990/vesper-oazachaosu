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
using System.Text;

namespace OazachaosuCore.Test.ApiControllersTests
{
    [TestFixture]
    public class UsersControllerPostTests
    {

        Mock<IHeaderElementProvider> headerElementProviderMock = new Mock<IHeaderElementProvider>();
        DbContextOptions<ApplicationDbContext> Options { get; set; }

        public UsersControllerPostTests()
        {

        }

        [SetUp]
        public void Setup()
        {
            Options = DatabaseUtil.GetOptions();
            DatabaseUtil.ClearDatabase(Options);
            headerElementProviderMock.Setup(x => x.GetElement(It.IsAny<HttpRequest>(), "userName")).Returns("user");
            headerElementProviderMock.Setup(x => x.GetElement(It.IsAny<HttpRequest>(), "password")).Returns("password");
        }

        [Test]
        public void Try_to_add_new_user()
        {
            using (var context = new ApplicationDbContext(Options))
            {
                JsonResult result = new UserController(new WordkiRepo(context)).Post2(headerElementProviderMock.Object) as JsonResult;
                Assert.NotNull(result, "Problem with get JsonResult");

                ApiResult apiResult = result.Value as ApiResult;
                Assert.NotNull(apiResult, "Problem with get ApiResult");

                Assert.AreEqual(ResultCode.Done, apiResult.Code);
                User user = apiResult.Object as User;
                Assert.NotNull(user, "Problem with get User");
                Assert.AreEqual("user", user.Name, "Problem with Name");
                Assert.AreEqual("password", user.Password, "Problem with Password");
                Assert.IsNotEmpty(user.ApiKey, "Problem with ApiKey");
                Assert.IsTrue(user.Id > 0, "Problem with Id");
            }
        }

        [Test]
        public void Try_to_add_second_user()
        {
            DatabaseUtil.SetUser(Options);
            using (var context = new ApplicationDbContext(Options))
            {
                headerElementProviderMock.Setup(x => x.GetElement(It.IsAny<HttpRequest>(), "userName")).Returns("user2");
                headerElementProviderMock.Setup(x => x.GetElement(It.IsAny<HttpRequest>(), "password")).Returns("password2");
                JsonResult result = new UserController(new WordkiRepo(context)).Post2(headerElementProviderMock.Object) as JsonResult;
                Assert.NotNull(result, "Problem with get JsonResult");

                ApiResult apiResult = result.Value as ApiResult;
                Assert.NotNull(apiResult, "Problem with get ApiResult");

                Assert.AreEqual(ResultCode.Done, apiResult.Code);
                User user = apiResult.Object as User;
                Assert.NotNull(user, "Problem with get User");
                Assert.AreEqual("user2", user.Name, "Problem with Name");
                Assert.AreEqual("password2", user.Password, "Problem with Password");
                Assert.IsNotEmpty(user.ApiKey, "Problem with ApiKey");
                Assert.IsTrue(user.Id > 0, "Problem with Id");
            }

        }

    }
}
