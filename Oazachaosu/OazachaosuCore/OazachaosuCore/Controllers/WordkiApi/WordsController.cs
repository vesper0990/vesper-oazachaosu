using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using OazachaosuCore.Helpers;
using OazachaosuCore.Helpers.Respone;
using OazachaosuCore.Models.ApiViewModels;
using Repository;
using Repository.Model.DTOConverters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using WordkiModelCore.DTO;

namespace OazachaosuCore.Controllers
{
    [Route("[controller]")]
    public class WordsController : ApiControllerBase
    {

        private IWordkiRepo Repository { get; set; }

        public WordsController(IWordkiRepo repository) : base()
        {
            Repository = repository;
        }

        [HttpGet("{dateTime}/{apiKey}")]
        public IActionResult Get(DateTime dateTime, string apiKey)
        {
            User user = Repository.GetUsers().SingleOrDefault(x => x.ApiKey.Equals(apiKey));
            if (user == null)
            {
                return StatusCode((int)HttpStatusCode.Unauthorized);
            }
            return Json(WordConverter.GetDTOsFromWords(Repository.GetWords().Where(x => x.UserId == user.Id && x.LastChange > dateTime)));
        }

        [HttpPost]
        public IActionResult Post([FromBody] PostWordsViewModel data)
        {
            DateTime now = DateTime.Now;
            User user = Repository.GetUsers().SingleOrDefault(x => x.ApiKey.Equals(data.ApiKey));
            if (user == null)
            {
                return StatusCode((int)HttpStatusCode.Unauthorized);
            }
            IEnumerable<Word> words = WordConverter.GetWordsFromDTOs(data.Data);
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
            return Ok();
        }

        [HttpGet("Get3")]
        public IActionResult Get3([FromServices] IHeaderElementProvider headerElementProvider)
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

        [HttpPost("Post3")]
        public async Task<IActionResult> Post3([FromServices] IBodyProvider bodyProvider, [FromServices] IHeaderElementProvider headerElementProvider)
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
