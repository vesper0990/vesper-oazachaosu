using AutoMapper;
using Newtonsoft.Json;
using NUnit.Framework;
using OazachaosuCore.Mappers;
using Repository;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using WordkiModelCore.DTO;

namespace OazachaosuCore.Test.EndToEndTests.ApiTests
{
    [TestFixture]
    public class UserControllerTests : ApiTestBase
    {

        IMapper mapper = AutoMapperConfig.Initialize();

        public UserControllerTests() : base()
        {
        }

        [SetUp]
        public void SetUp()
        {
            DatabaseUtil.ClearDatabase(Options);
        }

        [Test]
        public async Task Try_to_get_user_if_database_is_empty()
        {
            var response = await client.GetAsync("Users/name/password");
            Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Test]
        public async Task Try_to_get_user_if_wrong_name()
        {
            DatabaseUtil.SetUser(Options);
            var response = await client.GetAsync($"Users/xxx/{DatabaseUtil.User.Password}");
            Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Test]
        public async Task Try_to_get_user_if_wrong_password()
        {
            DatabaseUtil.SetUser(Options);
            var response = await client.GetAsync($"Users/{DatabaseUtil.User.Name}/xxx");
            Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Test]
        public async Task Try_to_get_user_if_correct_data()
        {
            DatabaseUtil.SetUser(Options);
            var response = await client.GetAsync($"Users/{DatabaseUtil.User.Name}/{DatabaseUtil.User.Password}");
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            var responseString = await response.Content.ReadAsStringAsync();
            UserDTO userDto = JsonConvert.DeserializeObject<UserDTO>(responseString);
            Assert.NotNull(userDto);
            Assert.AreEqual(DatabaseUtil.User.Id, userDto.Id);
            Assert.AreEqual(DatabaseUtil.User.Password, userDto.Password);
            Assert.AreEqual(DatabaseUtil.User.ApiKey, userDto.ApiKey);
            Assert.AreEqual(DatabaseUtil.User.Name, userDto.Name);
        }

        [Test]
        public async Task Try_to_post_user_without_name()
        {
            User user = DatabaseUtil.GetUser(id: 0, apiKey: null, name: null);
            HttpContent content = new StringContent(JsonConvert.SerializeObject(mapper.Map<User, UserDTO>(user)), Encoding.UTF8, "application/json");
            var response = await client.PostAsync("Users", content);
            Assert.AreEqual(HttpStatusCode.InternalServerError, response.StatusCode);
        }

        [Test]
        public async Task Try_to_post_user_without_password()
        {
            User user = DatabaseUtil.GetUser(id: 0, apiKey: null, password: null);
            HttpContent content = new StringContent(JsonConvert.SerializeObject(mapper.Map<User, UserDTO>(user)), Encoding.UTF8, "application/json");
            var response = await client.PostAsync("Users", content);
            Assert.AreEqual(HttpStatusCode.InternalServerError, response.StatusCode);
        }

        [Test]
        public async Task Try_to_post_user_with_proper_data()
        {
            User user = DatabaseUtil.GetUser(id: 0, apiKey: null);
            HttpContent content = new StringContent(JsonConvert.SerializeObject(mapper.Map<User, UserDTO>(user)), Encoding.UTF8, "application/json");
            var response = await client.PostAsync("Users", content);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);

            var responseString = await response.Content.ReadAsStringAsync();
            UserDTO userDto = JsonConvert.DeserializeObject<UserDTO>(responseString);
            Assert.NotNull(userDto);
            Assert.AreEqual(user.Name, userDto.Name);
            Assert.AreEqual(user.Password, userDto.Password);
            Assert.AreNotEqual(0, userDto.Id);
            Assert.NotNull(userDto.ApiKey);
        }

        [Test]
        public async Task Try_to_post_user_if_exist()
        {
            DatabaseUtil.SetUser(Options);
            User userToSend = DatabaseUtil.GetUser(id: 0, apiKey: null);
            HttpContent content = new StringContent(JsonConvert.SerializeObject(mapper.Map<User, UserDTO>(userToSend)), Encoding.UTF8, "application/json");
            var response = await client.PostAsync("Users", content);
            Assert.AreEqual(HttpStatusCode.InternalServerError, response.StatusCode);
        }

        [Test]
        public async Task Try_to_check_if_user_exist_if_it_not_exist()
        {
            var response = await client.GetAsync($"Users/xxx");
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            var responseString = await response.Content.ReadAsStringAsync();
            bool isExists = JsonConvert.DeserializeObject<bool>(responseString);
            Assert.IsFalse(isExists);
        }

        [Test]
        public async Task Try_to_check_if_user_exist_if_it_exist()
        {
            DatabaseUtil.SetUser(Options);
            var response = await client.GetAsync($"Users/{DatabaseUtil.User.Name}");
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            var responseString = await response.Content.ReadAsStringAsync();
            bool isExists = JsonConvert.DeserializeObject<bool>(responseString);
            Assert.IsTrue(isExists);
        }


    }
}
