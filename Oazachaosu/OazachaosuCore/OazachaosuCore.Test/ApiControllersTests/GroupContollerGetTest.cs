using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;
using OazachaosuCore.Controllers;
using Repository;
using System.Collections.Generic;
using System.Linq;

namespace OazachaosuCore.Test.ApiControllersTests
{
    [TestFixture]
    public class GroupContollerGetTest
    {

        GroupsController controller;

        public GroupContollerGetTest()
        {
            Utility.GroupCount = 2;
            Utility.WordCount = 2;
            Utility.ResultCount = 2;
        }

        [SetUp]
        public void SetUp()
        {
            controller = new GroupsController(DatabaseUtil.GetWordkiRepoWithDate());
        }

        [Test]
        public void Check_group_count()
        {
            JsonResult result = controller.Get() as JsonResult;
            Assert.NotNull(result);

            IEnumerable<Group> groups = result.Value as IEnumerable<Group>;
            Assert.AreEqual(Utility.GroupCount, groups.Count());
        }

        [Test]
        public void Check_words_count()
        {
            JsonResult result = controller.Get() as JsonResult;
            IEnumerable<Group> groups = result.Value as IEnumerable<Group>;
            foreach(Group group in groups)
            {
                Assert.AreEqual(Utility.WordCount, group.Words.Count);
            }
        }

        [Test]
        public void Check_result_count()
        {
            JsonResult result = controller.Get() as JsonResult;
            IEnumerable<Group> groups = result.Value as IEnumerable<Group>;
            foreach (Group group in groups)
            {
                Assert.AreEqual(Utility.ResultCount, group.Results.Count);
            }
        }

    }
}
