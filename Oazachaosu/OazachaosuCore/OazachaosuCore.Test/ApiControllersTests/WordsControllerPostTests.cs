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
using System.Text;
using System.Threading.Tasks;

namespace OazachaosuCore.Test.ApiControllersTests
{
    [TestFixture]
    public class WordsControllerPostTests
    {
        Mock<IBodyProvider> bodyProviderMock = new Mock<IBodyProvider>();
        Mock<IHeaderElementProvider> headerElementProviderMock = new Mock<IHeaderElementProvider>();
        string postData = "[{\"Id\":1,\"Gid\":1,\"L1\":\"Language\",\"L2\":\"Język\",\"L1C\":\"Example\",\"L2C\":\"Przykład\",\"D\":0,\"V\":true,\"S\":2147483647,\"IS\":false,\"RC\":0,\"LR\":\"0001-01-01T00:00:00\",\"C\":\"Comment for word 1\"},{\"Id\":2,\"Gid\":1,\"L1\":\"Language\",\"L2\":\"Język\",\"L1C\":\"Example\",\"L2C\":\"Przykład\",\"D\":0,\"V\":true,\"S\":2147483647,\"IS\":false,\"RC\":0,\"LR\":\"0001-01-01T00:00:00\",\"C\":\"Comment for word 2\"}]";
        DbContextOptions<ApplicationDbContext> Options { get; set; }


        public WordsControllerPostTests()
        {
            bodyProviderMock.Setup(x => x.GetBody(It.IsAny<HttpRequest>())).Returns(postData);
            bodyProviderMock.Setup(x => x.GetBodyAsync(It.IsAny<HttpRequest>())).Returns(Task.FromResult<string>(postData));
            headerElementProviderMock.Setup(x => x.GetElement(It.IsAny<HttpRequest>(), "apikey")).Returns(DatabaseUtil.User.ApiKey);
        }

        [SetUp]
        public void SetUp()
        {
            Options = DatabaseUtil.GetOptions();
            DatabaseUtil.ClearDatabase(Options);
            DatabaseUtil.SetUser(Options);
            using (var context = new ApplicationDbContext(Options))
            {
                context.Groups.Add(new Group() { Id = 1, UserId = DatabaseUtil.User.Id });
                context.SaveChanges();
            }
        }

        [Test]
        public void Post_data_to_empty_database()
        {
            using (var context = new ApplicationDbContext(Options))
            {
                JsonResult jsonResult = new WordsController(new WordkiRepo(context)).Post(bodyProviderMock.Object, headerElementProviderMock.Object).Result as JsonResult;
                Assert.NotNull(jsonResult, "JsonResult is null");
                ApiResult apiResult = jsonResult.Value as ApiResult;
                Assert.NotNull(apiResult, "ApiResult is null");
                Assert.AreEqual(ResultCode.Done, apiResult.Code);
            }
            using(var context = new ApplicationDbContext(Options))
            {
                Assert.AreEqual(2, context.Words.Count());
            }
        }

        [Test]
        public void Post_data_to_not_empty_database()
        {
            using (var context = new ApplicationDbContext(Options))
            {
                Word word = new Word()
                {
                    Id = 100,
                    GroupId = 1,
                    UserId = DatabaseUtil.User.Id,
                };
                context.Words.Add(word);
                context.SaveChanges();
            }

            using (var context = new ApplicationDbContext(Options))
            {
                JsonResult jsonResult = new WordsController(new WordkiRepo(context)).Post(bodyProviderMock.Object, headerElementProviderMock.Object).Result as JsonResult;
                Assert.NotNull(jsonResult);
                ApiResult apiResult = jsonResult.Value as ApiResult;
                Assert.NotNull(apiResult);
                Assert.AreEqual(ResultCode.Done, apiResult.Code);
                Assert.AreEqual(3, context.Words.Count());
            }

        }

        [Test]
        public void Post_data_to_update()
        {
            using (var context = new ApplicationDbContext(Options))
            {
                Word word = new Word()
                {
                    Id = 1,
                    GroupId = 1,
                    UserId = DatabaseUtil.User.Id,
                    Language1 = "asdf",
                };
                context.Words.Add(word);
                context.SaveChanges();
            }
            using (var context = new ApplicationDbContext(Options))
            {
                JsonResult jsonResult = new WordsController(new WordkiRepo(context)).Post(bodyProviderMock.Object, headerElementProviderMock.Object).Result as JsonResult;
                Assert.NotNull(jsonResult);
                ApiResult apiResult = jsonResult.Value as ApiResult;
                Assert.NotNull(apiResult);
                Assert.AreEqual(ResultCode.Done, apiResult.Code);
                Assert.AreEqual(2, context.Words.Count());

                Word wordFromDb = context.Words.SingleOrDefault(x => x.Id == 1);
                Assert.NotNull(wordFromDb);
                Assert.AreEqual("Language", wordFromDb.Language1);
            }
        }

    }
}
