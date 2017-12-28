using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;
using OazachaosuCore.Controllers;

namespace OazachaosuCore.Test
{
    [TestFixture]
    public class GroupControllerTests
    {

        GroupsController controller;

        [SetUp]
        public void SetUp()
        {
            controller = new GroupsController(DatabaseUtil.GetWordkiRepoWithDate());
        }

        [Test]
        public void Test()
        {
            var result = controller.Get() as JsonResult;
            Assert.NotNull(result.Value);
        }

    }
}
