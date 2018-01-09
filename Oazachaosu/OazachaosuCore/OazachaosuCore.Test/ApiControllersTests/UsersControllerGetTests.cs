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
    public class UsersControllerGetTests
    {

        Mock<IHeaderElementProvider> headerElementProviderMock = new Mock<IHeaderElementProvider>();
        DbContextOptions<ApplicationDbContext> Options { get; set; }

        public UsersControllerGetTests()
        {
        }

        [SetUp]
        public void SetUp()
        {
            Options = DatabaseUtil.GetOptions();
            DatabaseUtil.SetUser(Options);
            headerElementProviderMock.Setup(x => x.GetElement(It.IsAny<HttpRequest>(), "userName")).Returns("user");
            headerElementProviderMock.Setup(x => x.GetElement(It.IsAny<HttpRequest>(), "password")).Returns("password");
        }

        [Test]
        public void Try_get_user_with_wrong_name()
        {
            using (var context = new ApplicationDbContext(Options))
            {
                headerElementProviderMock.Setup(x => x.GetElement(It.IsAny<HttpRequest>(), "userName")).Returns("");
                JsonResult result = new UserController(new WordkiRepo(context)).Get(headerElementProviderMock.Object) as JsonResult;
                Assert.NotNull(result);

                ApiResult apiResult = result.Value as ApiResult;
                Assert.NotNull(apiResult);

                Assert.AreEqual(ResultCode.UserNotFound, apiResult.Code);
            }
        }

        [Test]
        public void Try_get_user_with_wrong_password()
        {
            using (var context = new ApplicationDbContext(Options))
            {
                headerElementProviderMock.Setup(x => x.GetElement(It.IsAny<HttpRequest>(), "password")).Returns("");
                JsonResult result = new UserController(new WordkiRepo(context)).Get(headerElementProviderMock.Object) as JsonResult;
                Assert.NotNull(result);

                ApiResult apiResult = result.Value as ApiResult;
                Assert.NotNull(apiResult);

                Assert.AreEqual(ResultCode.AuthorizationError, apiResult.Code);
            }
        }

        [Test]
        public void Try_get_user()
        {
            using (var context = new ApplicationDbContext(Options))
            {
                JsonResult result = new UserController(new WordkiRepo(context)).Get(headerElementProviderMock.Object) as JsonResult;
                Assert.NotNull(result);

                ApiResult apiResult = result.Value as ApiResult;
                Assert.NotNull(apiResult);

                Assert.AreEqual(ResultCode.Done, apiResult.Code);

                User user = apiResult.Object as User;
                Assert.NotNull(user);
            }
        }
    }
}
