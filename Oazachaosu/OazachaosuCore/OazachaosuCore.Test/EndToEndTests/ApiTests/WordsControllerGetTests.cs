using Newtonsoft.Json;
using NUnit.Framework;
using OazachaosuCore.Data;
using Repository;
using Repository.Model.DTOConverters;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace OazachaosuCore.Test.EndToEndTests.ApiTests
{
    [TestFixture]
    public class WordsControllerGetTests : ApiTestBase
    {
        public WordsControllerGetTests() : base()
        {
        }

        [SetUp]
        public void SetUp()
        {
            DatabaseUtil.ClearDatabase(Options);
            DatabaseUtil.SetUser(Options);
        }

        [Test]
        public async Task Try_to_get_words_with_right_user()
        {
            var response = await client.GetAsync("Words/1990-01-01/apikey");
            response.EnsureSuccessStatusCode();

            var responseString = await response.Content.ReadAsStringAsync();

            // Assert
            Assert.AreEqual("[]", responseString);
        }

        [Test]
        public async Task Try_to_get_words_without_right_user()
        {
            var response = await client.GetAsync("Words/1990-01-01/0");
            Assert.AreEqual(HttpStatusCode.Unauthorized, response.StatusCode);
        }

        [Test]
        public async Task Try_to_get_word()
        {
            Group groupToAdd = DatabaseUtil.GetGroup();
            Word wordToAdd = DatabaseUtil.GetWord();
            wordToAdd.Id = 1;
            using (var context = new ApplicationDbContext(Options))
            {
                context.Groups.Add(groupToAdd);
                context.SaveChanges();
            }
            using (var context = new ApplicationDbContext(Options))
            {
                context.Words.Add(wordToAdd);
                context.SaveChanges();
            }
            var response = await client.GetAsync("Words/1990-01-01/apiKey");
            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();
            Assert.AreEqual(JsonConvert.SerializeObject(WordConverter.GetDTOsFromWords(new List<Word>() { wordToAdd })), responseString);
        }
    }
}
