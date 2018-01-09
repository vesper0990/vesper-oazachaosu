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
    public class ResultsControllerPostTests
    {
        Mock<IBodyProvider> bodyProviderMock = new Mock<IBodyProvider>();
        Mock<IHeaderElementProvider> headerElementProviderMock = new Mock<IHeaderElementProvider>();
        string postData = "[{\"id\":1,\"parentId\":1,\"correct\":10,\"accepted\":10,\"wrong\":10,\"invisibilities\":10,\"timeCount\":10,\"translationDirection\":1,\"lessonType\":1,\"dateTime\":\"1990-09-24T00:00:00\",\"state\":2147483647},{\"id\":2,\"parentId\":1,\"correct\":10,\"accepted\":10,\"wrong\":10,\"invisibilities\":10,\"timeCount\":10,\"translationDirection\":1,\"lessonType\":1,\"dateTime\":\"1990-09-24T00:00:00\",\"state\":2147483647}]";
        DbContextOptions<ApplicationDbContext> Options { get; set; }


        public ResultsControllerPostTests()
        {
            bodyProviderMock.Setup(x => x.GetBody(It.IsAny<HttpRequest>())).Returns(postData);
            bodyProviderMock.Setup(x => x.GetBodyAsync(It.IsAny<HttpRequest>())).Returns(Task.FromResult<string>(postData));
            headerElementProviderMock.Setup(x => x.GetElement(It.IsAny<HttpRequest>(), "apikey")).Returns(DatabaseUtil.User.ApiKey);
        }

        [SetUp]
        public void SetUp()
        {
            Options = DatabaseUtil.GetOptions();
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
                JsonResult jsonResult = new ResultsController(new WordkiRepo(context)).Post(bodyProviderMock.Object, headerElementProviderMock.Object).Result as JsonResult;
                Assert.NotNull(jsonResult, "JsonResult is null");
                ApiResult apiResult = jsonResult.Value as ApiResult;
                Assert.NotNull(apiResult, "ApiResult is null");
                Assert.AreEqual(ResultCode.Done, apiResult.Code);
                Assert.AreEqual(2, context.Results.Count());
            }
        }

        [Test]
        public void Post_data_to_not_empty_database()
        {
            using (var context = new ApplicationDbContext(Options))
            {
                Result Result = new Result()
                {
                    Id = 100,
                    ParentId = 1,
                };
                context.Results.Add(Result);
                context.SaveChanges();
            }
            using (var context = new ApplicationDbContext(Options))
            {
                JsonResult jsonResult = new ResultsController(new WordkiRepo(context)).Post(bodyProviderMock.Object, headerElementProviderMock.Object).Result as JsonResult;
                Assert.NotNull(jsonResult);
                ApiResult apiResult = jsonResult.Value as ApiResult;
                Assert.NotNull(apiResult);
                Assert.AreEqual(ResultCode.Done, apiResult.Code);
                Assert.AreEqual(3, context.Results.Count());
            }

        }

        [Test]
        public void Post_data_to_update()
        {
            using (var context = new ApplicationDbContext(Options))
            {
                Result Result = new Result()
                {
                    Id = 1,
                    ParentId = 1,
                    Correct = 999,
                };
                context.Results.Add(Result);
                context.SaveChanges();
            }
            using (var context = new ApplicationDbContext(Options))
            {
                JsonResult jsonResult = new ResultsController(new WordkiRepo(context)).Post(bodyProviderMock.Object, headerElementProviderMock.Object).Result as JsonResult;
                Assert.NotNull(jsonResult);
                ApiResult apiResult = jsonResult.Value as ApiResult;
                Assert.NotNull(apiResult);
                Assert.AreEqual(ResultCode.Done, apiResult.Code);
                Assert.AreEqual(2, context.Results.Count());

                Result ResultFromDb = context.Results.SingleOrDefault(x => x.Id == 1);
                Assert.NotNull(ResultFromDb);
                Assert.AreEqual(10, ResultFromDb.Correct);
            }
        }

    }
}
