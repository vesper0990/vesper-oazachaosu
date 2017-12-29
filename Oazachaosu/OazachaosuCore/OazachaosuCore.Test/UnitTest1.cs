using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using OazachaosuCore.Controllers;
using OazachaosuCore.Helpers;
using Repository;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OazachaosuCore.Test
{
    [TestFixture]
    public class UnitTest1
    {

        Mock<IBodyProvider> mock = new Mock<IBodyProvider>();

        [Test]
        public void TestMethod1()
        {
            IWordkiRepo repo = DatabaseUtil.GetWordkiRepoWithDate();
            IList<Group> groups = repo.GetGroups().ToList();

            //mock.Setup(x => x.GetBodyAsync()).Returns(Task.FromResult("test"));
            ResultsController controller = new ResultsController(DatabaseUtil.GetWordkiRepoWithDate());
            JsonResult result = controller.Get() as JsonResult;
            
            //Task<IActionResult> result = controller.Post(mock.Object);
        }
    }
}
