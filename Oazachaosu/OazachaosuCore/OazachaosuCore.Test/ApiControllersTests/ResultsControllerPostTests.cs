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
        string postData = "[{\"Id\":1,\"Gid\":1,\"C\":10,\"A\":10,\"W\":10,\"IV\":10,\"TC\":10,\"TD\":1,\"LT\":1,\"DT\":\"1990-09-24T00:00:00\",\"S\":2147483647},{\"Id\":2,\"Gid\":1,\"C\":10,\"A\":10,\"W\":10,\"IV\":10,\"TC\":10,\"TD\":1,\"LT\":1,\"DT\":\"1990-09-24T00:00:00\",\"S\":2147483647}]";
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
                    GroupId = 1,
                    UserId = DatabaseUtil.User.Id,
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
                    GroupId = context.Groups.First().Id,
                    UserId = DatabaseUtil.User.Id,
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

        [Test]
        public void Post_date_with_two_users()
        {
            using (var context = new ApplicationDbContext(Options))
            {
                User user2 = new User()
                {
                    Id = 3,
                    ApiKey = "3",
                    Name = "3",
                    Password = "3",
                };
                context.Users.Add(user2);
                Group groupForUser2 = new Group()
                {
                    Id = 1,
                    UserId = 3,
                };
                Result result = new Result()
                {
                    Id = 1,
                    GroupId = groupForUser2.Id,
                };
                groupForUser2.Results.Add(result);
                context.Groups.Add(groupForUser2);
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

    }
}
