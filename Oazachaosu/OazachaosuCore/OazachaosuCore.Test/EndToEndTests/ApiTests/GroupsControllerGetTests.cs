using Newtonsoft.Json;
using NUnit.Framework;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using WordkiModelCore.DTO;

namespace OazachaosuCore.Test.EndToEndTests.ApiTests
{
    [TestFixture]
    public class GroupsControllerGetTests : ApiTestBase
    {

        public GroupsControllerGetTests() : base()
        {
        }

        [SetUp]
        public void SetUp()
        {
            DatabaseUtil.ClearDatabase(Options);
            DatabaseUtil.SetUser(Options);
            DatabaseUtil.SetGroup(Options);
        }


        [Test]
        public async Task Try_to_get_groups_without_datatime()
        {
            var response = await client.GetAsync($"Groups/{DatabaseUtil.User.ApiKey}");
            Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Test]
        public async Task Try_to_get_groups_without_apikey()
        {
            var response = await client.GetAsync($"Groups/1990-01-01");
            Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Test]
        public async Task Try_to_get_group_with_wrong_apikey()
        {
            var response = await client.GetAsync($"Groups/1990-01-01/xxx");
            Assert.AreEqual(HttpStatusCode.InternalServerError, response.StatusCode);
        }

        [Test]
        public async Task Try_to_get_groups_with_wrong_date()
        {
            var response = await client.GetAsync($"Groups/1990_99_01/{DatabaseUtil.User.ApiKey}");
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            var responseString = await response.Content.ReadAsStringAsync();
            IEnumerable<GroupDTO> groups = JsonConvert.DeserializeObject<IEnumerable<GroupDTO>>(responseString);
            Assert.NotNull(groups);
            Assert.AreEqual(1, groups.Count());
        }

        [Test]
        public async Task Try_to_get_group_from_empty_database()
        {
            DatabaseUtil.SetUser(Options, new Repository.User() { Id = 99, ApiKey = "xxx", Password = "xxx", Name = "xxx" });
            var response = await client.GetAsync($"Groups/1990-01-01/xxx");
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            var responseString = await response.Content.ReadAsStringAsync();
            IEnumerable<GroupDTO> groups = JsonConvert.DeserializeObject<IEnumerable<GroupDTO>>(responseString);
            Assert.NotNull(groups);
            Assert.AreEqual(0, groups.Count());
        }

        [Test]
        public async Task Try_to_get_groups_check_last_change()
        {
            Group nextGroup = DatabaseUtil.GetGroup(id: 2);
            nextGroup.LastChange = new DateTime(2018, 1, 1);
            DatabaseUtil.SetGroup(Options, nextGroup);
            var response = await client.GetAsync($"Groups/2017-01-01/{DatabaseUtil.User.ApiKey}");
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            var responseString = await response.Content.ReadAsStringAsync();
            IEnumerable<GroupDTO> groups = JsonConvert.DeserializeObject<IEnumerable<GroupDTO>>(responseString);
            Assert.NotNull(groups);
            Assert.AreEqual(1, groups.Count());
        }
    }
}
