using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using OazachaosuCore.Controllers;
using OazachaosuCore.Helpers;
using OazachaosuCore.Helpers.Respone;
using System.Threading.Tasks;
using System.Linq;
using Repository;
using Microsoft.EntityFrameworkCore;
using OazachaosuCore.Data;

namespace OazachaosuCore.Test.ApiControllersTests
{
    [TestFixture]
    public class GroupControllerPostTest
    {
        Mock<IBodyProvider> bodyProviderMock = new Mock<IBodyProvider>();
        Mock<IHeaderElementProvider> headerElementProviderMock = new Mock<IHeaderElementProvider>();
        string postData = "[{\"Id\":1,\"N\":\"Group 1\",\"L1\":1,\"L2\":2,\"S\":2147483647,\"CD\":\"2017-12-29T13:03:18.486645\"},{\"Id\":2,\"N\":\"Group 1\",\"L1\":1,\"L2\":2,\"S\":2147483647,\"CD\":\"2017-12-29T13:03:18.486645\"}]";//
        DbContextOptions<ApplicationDbContext> Options { get; set; }

        public GroupControllerPostTest()
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
        }

        [Test]
        public void Post_data_to_empty_database()
        {
            using (var context = new ApplicationDbContext(Options))
            {
                JsonResult jsonResult = new GroupsController(new WordkiRepo(context)).Post(bodyProviderMock.Object, headerElementProviderMock.Object).Result as JsonResult;
                Assert.NotNull(jsonResult);
                ApiResult apiResult = jsonResult.Value as ApiResult;
                Assert.NotNull(apiResult);
                Assert.AreEqual(ResultCode.Done, apiResult.Code);
                Assert.AreEqual(2, context.Groups.Count());
            }
        }

        [Test]
        public void Post_data_to_not_empty_database()
        {
            using (var context = new ApplicationDbContext(Options))
            {
                Group group = new Group()
                {
                    Id = 100,
                };
                context.Groups.Add(group);
                context.SaveChanges();
            }

            using (var context = new ApplicationDbContext(Options))
            {
                JsonResult jsonResult = new GroupsController(new WordkiRepo(context)).Post(bodyProviderMock.Object, headerElementProviderMock.Object).Result as JsonResult;
                Assert.NotNull(jsonResult);
                ApiResult apiResult = jsonResult.Value as ApiResult;
                Assert.NotNull(apiResult);
                Assert.AreEqual(ResultCode.Done, apiResult.Code);
                Assert.AreEqual(3, context.Groups.Count());
            }

        }

        [Test]
        public void Post_data_to_update()
        {

            using (var context = new ApplicationDbContext(Options))
            {
                Group group = new Group()
                {
                    Id = 1,
                    Name = "jakas",
                    UserId = DatabaseUtil.User.Id,
                };
                context.Groups.Add(group);
                context.SaveChanges();
            }
            using(var context  = new ApplicationDbContext(Options))
            {
                JsonResult jsonResult = new GroupsController(new WordkiRepo(context)).Post(bodyProviderMock.Object, headerElementProviderMock.Object).Result as JsonResult;
                Assert.NotNull(jsonResult);
                ApiResult apiResult = jsonResult.Value as ApiResult;
                Assert.NotNull(apiResult);
                Assert.AreEqual(ResultCode.Done, apiResult.Code);
                Assert.AreEqual(2, context.Groups.Count());

                Group groupFromDb = context.Groups.SingleOrDefault(x => x.Id == 1);
                Assert.NotNull(groupFromDb);
                Assert.AreEqual("Group 1", groupFromDb.Name);
            }
        }
    }
}
