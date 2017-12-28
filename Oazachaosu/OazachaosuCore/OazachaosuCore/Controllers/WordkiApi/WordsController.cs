using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using OazachaosuCore.Data;
using Repository;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OazachaosuCore.Controllers.WordkiApi
{
    [Route("Words")]
    public class WordsController : ApiControllerBase
    {

        private IWordkiRepo Repository { get; set; }

        public WordsController(IWordkiRepo repository) : base()
        {
            Repository = repository;
        }

        [HttpGet("")]
        public IActionResult Get()
        {
            var list = Repository.GetWords();
            IActionResult result = new JsonResult(list);
            return result;
        }

        [HttpGet("{userId}")]
        public IActionResult Get(long userId)
        {
            IActionResult result = new JsonResult(Repository.GetWords(userId));
            return result;
        }

        [HttpPost]
        public async Task<IActionResult> Post()
        {
            string content = await GetContnet();
            IList<Word> words = JsonConvert.DeserializeObject<List<Word>>(content);
            IQueryable<Word> dbWords = Repository.GetWords();
            foreach (Word word in words)
            {
                if(dbWords.Any(x => x.Id == word.Id))
                {
                    Repository.UpdateWord(word);
                }
                else
                {
                    word.Group = Repository.GetGroup(word.ParentId);
                    //Repository.GetGroup(word.ParentId).AddWord(word);
                    Repository.Context.Update(word);
                }
            }
            Repository.Context.SaveChanges();
            return new ContentResult()
            {
                Content = "Ok",
            };
        }

    }
}
