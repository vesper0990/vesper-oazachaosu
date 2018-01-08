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

namespace OazachaosuCore.Test.ApiControllersTests
{
    [TestFixture]
    public class GroupControllerPostTest
    {
        GroupsController controller;
        Mock<IBodyProvider> bodyProviderMock = new Mock<IBodyProvider>();
        Mock<IHeaderElementProvider> headerElementProviderMock = new Mock<IHeaderElementProvider>();
        string postData = "[{\"id\":1,\"name\":\"Group 1\",\"language1\":1,\"language2\":2,\"state\":2147483647,\"creationDate\":\"2017-12-29T13:03:18.486645\"},{\"id\":2,\"name\":\"Group 1\",\"language1\":1,\"language2\":2,\"state\":2147483647,\"creationDate\":\"2017-12-29T13:03:18.486645\"}]";

        public GroupControllerPostTest()
        {
            bodyProviderMock.Setup(x => x.GetBody(It.IsAny<HttpRequest>())).Returns(postData);
            bodyProviderMock.Setup(x => x.GetBodyAsync(It.IsAny<HttpRequest>())).Returns(Task.FromResult<string>(postData));
            headerElementProviderMock.Setup(x => x.GetElement(It.IsAny<HttpRequest>(), "apikey")).Returns(DatabaseUtil.User.ApiKey);
        }

        [SetUp]
        public void SetUp()
        {
            controller = new GroupsController(DatabaseUtil.GetEmptyWordkiRepo());
            DatabaseUtil.Context.Users.Add(DatabaseUtil.User);
            DatabaseUtil.Context.SaveChanges();
        }

        [Test]
        public void Post_data_to_empty_database()
        {
            JsonResult jsonResult = controller.Post(bodyProviderMock.Object, headerElementProviderMock.Object).Result as JsonResult;
            Assert.NotNull(jsonResult);
            ApiResult apiResult = jsonResult.Value as ApiResult;
            Assert.NotNull(apiResult);
            Assert.AreEqual(ResultCode.Done, apiResult.Code);
            Assert.AreEqual(2, DatabaseUtil.Context.Groups.Count());
        }

        [Test]
        public void Post_data_to_not_empty_database()
        {
            Group group = new Group()
            {
                Id = 100,
            };
            DatabaseUtil.Context.Groups.Add(group);
            DatabaseUtil.Context.SaveChanges();

            JsonResult jsonResult = controller.Post(bodyProviderMock.Object, headerElementProviderMock.Object).Result as JsonResult;
            Assert.NotNull(jsonResult);
            ApiResult apiResult = jsonResult.Value as ApiResult;
            Assert.NotNull(apiResult);
            Assert.AreEqual(ResultCode.Done, apiResult.Code);
            Assert.AreEqual(3, DatabaseUtil.Context.Groups.Count());
        }

        [Test]
        public void Post_data_to_update()
        {
            Group group = new Group()
            {
                Id = 1,
                Name = "jakas",
            };
            DatabaseUtil.Context.Groups.Add(group);
            DatabaseUtil.Context.SaveChanges();
            JsonResult jsonResult = controller.Post(bodyProviderMock.Object, headerElementProviderMock.Object).Result as JsonResult;
            Assert.NotNull(jsonResult);
            ApiResult apiResult = jsonResult.Value as ApiResult;
            Assert.NotNull(apiResult);
            Assert.AreEqual(ResultCode.Done, apiResult.Code);
            Assert.AreEqual(2, DatabaseUtil.Context.Groups.Count());

            Group groupFromDb = DatabaseUtil.Context.Groups.SingleOrDefault(x => x.Id == 1);
            Assert.NotNull(groupFromDb);
            Assert.AreEqual("Group 1", groupFromDb.Name);
        }
    }
}
