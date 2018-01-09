using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using OazachaosuCore.Helpers;
using OazachaosuCore.Helpers.Respone;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OazachaosuCore.Controllers
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
        public IActionResult Get([FromServices] IHeaderElementProvider headerElementProvider)
        {
            ApiResult result = new ApiResult();
            DateTime dateTime = DateTime.Parse(headerElementProvider.GetElement(Request, "dateTime"));
            string apiKey = headerElementProvider.GetElement(Request, "apikey");
            User user = Repository.GetUsers().SingleOrDefault(x => x.ApiKey.Equals(apiKey));
            if (user == null)
            {
                result.Message = "User not found.";
                result.Code = ResultCode.AuthorizationError;
                return new JsonResult(result);
            }
            result.Object = Repository.GetWords().Where(x => x.Group.UserId == user.Id && x.LastChange > dateTime);
            result.Code = ResultCode.Done;
            return new JsonResult(result);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromServices] IBodyProvider bodyProvider, [FromServices] IHeaderElementProvider headerElementProvider)
        {
            DateTime now = DateTime.Now;
            ApiResult result = new ApiResult();
            string apiKey = headerElementProvider.GetElement(Request, "apikey");
            User user = Repository.GetUsers().SingleOrDefault(x => x.ApiKey.Equals(apiKey));
            if (user == null)
            {
                result.Code = ResultCode.AuthorizationError;
                result.Message = "User not found";
                return new JsonResult(result);
            }
            string content = await bodyProvider.GetBodyAsync(Request);
            IList<Word> words = JsonConvert.DeserializeObject<List<Word>>(content);
            IQueryable<Word> dbWords = Repository.GetWords();
            foreach (Word word in words)
            {
                word.LastChange = now;
                if (dbWords.Any(x => x.Id == word.Id))
                {
                    Repository.UpdateWord(word);
                }
                else
                {
                    Repository.GetGroup(word.ParentId).AddWord(word);
                    Repository.AddWord(word);
                }
            }
            Repository.SaveChanges();
            result.Code = ResultCode.Done;
            return new JsonResult(result);
        }

    }
}
