using AutoMapper;
using Newtonsoft.Json;
using NUnit.Framework;
using Oazachaosu.Api.Data;
using Oazachaosu.Api.Mappers;
using Oazachaosu.Core;
using Oazachaosu.Core.Common;
using OazachaosuCore.Models.ApiViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace OazachaosuCore.Test.EndToEndTests.ApiTests
{
    [TestFixture]
    public class GroupControllerPostTests : ApiTestBase
    {
        PostGroupViewModel post;
        IMapper mapper;
        public GroupControllerPostTests() : base()
        {
            mapper = AutoMapperConfig.Initialize();
        }

        [SetUp]
        public void SetUp()
        {
            DatabaseUtil.ClearDatabase(Options);
            DatabaseUtil.SetUser(Options);
            post = new PostGroupViewModel();
        }

        [Test]
        public async Task Try_to_post_groups_without_apikey()
        {
            post.Data = new List<GroupDTO> { mapper.Map<Group, GroupDTO>(DatabaseUtil.GetGroup()) };
            StringContent content = new StringContent(JsonConvert.SerializeObject(post), Encoding.UTF8, "application/json");
            var response = await client.PostAsync("Groups", content);
            Assert.AreEqual(HttpStatusCode.InternalServerError, response.StatusCode);
            var responseString = await response.Content.ReadAsStringAsync();
            ErrorDTO error = JsonConvert.DeserializeObject<ErrorDTO>(responseString);
            Assert.NotNull(error);
            Assert.AreEqual(ErrorCode.ApiKeyIsEmpty, error.Code);
        }

        [Test]
        public async Task Try_to_post_groups_not_existing()
        {
            post.Data = new List<GroupDTO> { mapper.Map<Group, GroupDTO>(DatabaseUtil.GetGroup()) };
            post.ApiKey = DatabaseUtil.User.ApiKey;
            StringContent content = new StringContent(JsonConvert.SerializeObject(post), Encoding.UTF8, "application/json");
            var response = await client.PostAsync("Groups", content);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);

            using (var context = new ApplicationDbContext(Options))
            {
                Assert.AreEqual(1, context.Groups.Count());
                Group groupFromDatabase = context.Groups.Single();
                Assert.AreNotEqual(0, groupFromDatabase.Id);
                Assert.AreNotEqual(0, groupFromDatabase.UserId);
            }
        }

        [Test]
        public async Task Try_to_post_groups_existing()
        {
            using (var context = new ApplicationDbContext(Options))
            {
                context.Groups.Add(DatabaseUtil.GetGroup());
                await context.SaveChangesAsync();
            }
            Group groupToUpdate = DatabaseUtil.GetGroup(name: "xxx", language1: LanguageType.Polish, language2: LanguageType.Russian);
            post.Data = new List<GroupDTO> { mapper.Map<Group, GroupDTO>(groupToUpdate) };
            post.ApiKey = DatabaseUtil.User.ApiKey;
            StringContent content = new StringContent(JsonConvert.SerializeObject(post), Encoding.UTF8, "application/json");
            var response = await client.PostAsync("Groups", content);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);

            using (var context = new ApplicationDbContext(Options))
            {
                Assert.AreEqual(1, context.Groups.Count());
                Group groupFromDatabase = context.Groups.Single();
                Assert.AreEqual(groupToUpdate.Name, groupFromDatabase.Name);
                Assert.AreEqual(groupToUpdate.Language1, groupFromDatabase.Language1);
                Assert.AreEqual(groupToUpdate.Language2, groupFromDatabase.Language2);
            }
        }

        [Test]
        public async Task Try_to_post_groups_one_existing_one_not()
        {
            using (var context = new ApplicationDbContext(Options))
            {
                context.Groups.Add(DatabaseUtil.GetGroup(id: 1));
                await context.SaveChangesAsync();
            }
            post.Data = new List<GroupDTO> { mapper.Map<Group, GroupDTO>(DatabaseUtil.GetGroup(id: 1)), mapper.Map<Group, GroupDTO>(DatabaseUtil.GetGroup(id: 2)) };
            post.ApiKey = DatabaseUtil.User.ApiKey;
            StringContent content = new StringContent(JsonConvert.SerializeObject(post), Encoding.UTF8, "application/json");
            var response = await client.PostAsync("Groups", content);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);

            using (var context = new ApplicationDbContext(Options))
            {
                Assert.AreEqual(2, context.Groups.Count());
            }
        }
    }
}
