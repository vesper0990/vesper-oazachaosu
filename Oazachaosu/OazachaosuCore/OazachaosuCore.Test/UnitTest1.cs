using NUnit.Framework;
using Repository;

namespace OazachaosuCore.Test
{
    [TestFixture]
    public class UnitTest1
    {
        [Test]
        public void TestMethod1()
        {
            IWordkiRepo repo = DatabaseUtil.GetWordkiRepoWithDate();
            var groups = repo.GetGroups();
        }
    }
}
