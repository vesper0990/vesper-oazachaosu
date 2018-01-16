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
    [Route("Results")]
    public class ResultsController : ApiControllerBase
    {
        private IWordkiRepo Repository { get; set; }

        public ResultsController(IWordkiRepo wordkiRepo) : base()
        {
            Repository = wordkiRepo;
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
            result.Object = ResultConverter.GetDTOsFromResults(Repository.GetResults().Where(x => x.UserId == user.Id && x.LastChange > dateTime));
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
                result.Message = "User not found.";
                return new JsonResult(result);
            }
            string content = await bodyProvider.GetBodyAsync(Request);
            IEnumerable<ResultDTO> DTOs = JsonConvert.DeserializeObject<IEnumerable<ResultDTO>>(content);
            IEnumerable<Result> results = ResultConverter.GetResultsFromDTOs(DTOs);
            IQueryable<Group> dbGroups = Repository.GetGroups(user.Id).Include(x => x.Results);
            foreach (Result item in results)
            {
                item.LastChange = now;
                item.UserId = user.Id;
                Group group = dbGroups.SingleOrDefault(x => x.Id == item.GroupId);
                if (group == null)
                {
                    continue;
                }
                if(group.Results.Any(x => x.Id == item.Id))
                {
                    Repository.UpdateResult(item);
                }
                else
                {
                    Repository.AddResult(item);
                }
            }
            await Repository.SaveChangesAsync();
            result.Code = ResultCode.Done;
            return new JsonResult(result);
        }

    }
}
