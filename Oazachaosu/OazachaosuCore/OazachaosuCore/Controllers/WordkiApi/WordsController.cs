using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using OazachaosuCore.Helpers;
using OazachaosuCore.Helpers.Respone;
using Repository;
using Repository.Model.DTOConverters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WordkiModelCore.DTO;

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
            result.Object = WordConverter.GetDTOsFromWords(Repository.GetWords().Where(x => x.UserId == user.Id && x.LastChange > dateTime));
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
            IEnumerable<WordDTO> DTOs = JsonConvert.DeserializeObject<IEnumerable<WordDTO>>(content);
            IEnumerable<Word> words = WordConverter.GetWordsFromDTOs(DTOs);
            IQueryable<Group> dbGroups = Repository.GetGroups(user.Id).Include(x => x.Words);
            foreach (Word word in words)
            {
                word.LastChange = now;
                word.UserId = user.Id;
                Group group = dbGroups.SingleOrDefault(x => x.Id == word.GroupId);
                if (group == null)
                {
                    continue;
                }
                if (group.Words.Any(x => x.Id == word.Id))
                {
                    Repository.UpdateWord(word);
                }
                else
                {
                    Repository.AddWord(word);
                }
            }
            Repository.SaveChanges();
            result.Code = ResultCode.Done;
            return new JsonResult(result);
        }

    }
}
