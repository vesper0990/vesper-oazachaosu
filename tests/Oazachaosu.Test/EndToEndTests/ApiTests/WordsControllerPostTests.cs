using AutoMapper;
using Newtonsoft.Json;
using NUnit.Framework;
using Oazachaosu.Api.Data;
using Oazachaosu.Api.Mappers;
using Oazachaosu.Api.Models.ApiViewModels;
using Oazachaosu.Core;
using Oazachaosu.Core.Common;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace OazachaosuCore.Test.EndToEndTests.ApiTests
{
    [TestFixture]
    public class WordsControllerPostTests : ApiTestBase
    {

        private readonly IMapper mapper;
        PostWordsViewModel post = new PostWordsViewModel();

        public WordsControllerPostTests() : base()
        {
            mapper = AutoMapperConfig.Initialize();
        }

        [SetUp]
        public void SetUp()
        {
            DatabaseUtil.ClearDatabase(Options);
            DatabaseUtil.SetUser(Options);
            DatabaseUtil.SetGroup(Options);
        }

        [Test]
        public async Task Try_to_post_word_without_apikey()
        {
            post.Data = mapper.Map<IEnumerable<Word>, IEnumerable<WordDTO>>(new List<Word>() { DatabaseUtil.GetWord() });
            post.ApiKey = "";
            StringContent content = new StringContent(JsonConvert.SerializeObject(post), Encoding.UTF8, "application/json");
            var response = await client.PostAsync("Words", content);
            Assert.AreEqual(HttpStatusCode.InternalServerError, response.StatusCode);
        }

        [Test]
        public async Task Try_to_post_word_if_not_exists()
        {
            Word wordToDatabase = DatabaseUtil.GetWord();
            post.Data = mapper.Map<IEnumerable<Word>, IEnumerable<WordDTO>>(new List<Word>() { wordToDatabase });
            post.ApiKey = DatabaseUtil.User.ApiKey;
            StringContent content = new StringContent(JsonConvert.SerializeObject(post), Encoding.UTF8, "application/json");
            var response = await client.PostAsync("Words", content);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            using (var context = new ApplicationDbContext(Options))
            {
                Assert.AreEqual(1, context.Words.Count());
                Word wordFromDatabase = context.Words.Single();
                Assert.AreNotEqual(0, wordFromDatabase.Id);
                Assert.AreNotEqual(0, wordFromDatabase.UserId);
            }
        }

        [Test]
        public async Task Try_to_post_word_if_exists()
        {
            using (var context = new ApplicationDbContext(Options))
            {
                context.Words.Add(DatabaseUtil.GetWord(id: 1));
                await context.SaveChangesAsync();
            }
            Word wordToDatabase = DatabaseUtil.GetWord(id: 1, language1: "xxx", language2: "yyy");
            post.Data = mapper.Map<IEnumerable<Word>, IEnumerable<WordDTO>>(new List<Word>() { wordToDatabase });
            post.ApiKey = DatabaseUtil.User.ApiKey;
            StringContent content = new StringContent(JsonConvert.SerializeObject(post), Encoding.UTF8, "application/json");
            var response = await client.PostAsync("Words", content);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            using (var context = new ApplicationDbContext(Options))
            {
                Assert.AreEqual(1, context.Words.Count());
                Word wordFromDatabase = context.Words.Single();
                Assert.AreEqual(wordToDatabase.Language1, wordFromDatabase.Language1);
                Assert.AreEqual(wordToDatabase.Language2, wordFromDatabase.Language2);
            }
        }

        [Test]
        public async Task Try_to_post_groups_one_existing_one_not()
        {
            using (var context = new ApplicationDbContext(Options))
            {
                context.Words.Add(DatabaseUtil.GetWord(id: 1));
                await context.SaveChangesAsync();
            }
            post.Data = mapper.Map<IEnumerable<Word>, IEnumerable<WordDTO>>(new List<Word>() { DatabaseUtil.GetWord(id: 1), DatabaseUtil.GetWord(id: 2) });
            post.ApiKey = DatabaseUtil.User.ApiKey;
            StringContent content = new StringContent(JsonConvert.SerializeObject(post), Encoding.UTF8, "application/json");
            var response = await client.PostAsync("Words", content);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);

            using (var context = new ApplicationDbContext(Options))
            {
                Assert.AreEqual(2, context.Words.Count());
            }
        }

    }
}
