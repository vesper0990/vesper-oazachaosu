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
            result.Object = Repository.GetResults().Where(x => x.Group.UserId == user.Id && x.LastChange > dateTime);
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
            IList<Result> results = JsonConvert.DeserializeObject<List<Result>>(content);
            IQueryable<Result> dbGroups = Repository.GetResults();
            foreach (Result item in results)
            {
                item.LastChange = now;
                if (dbGroups.Any(x => x.Id == item.Id))
                {
                    Repository.UpdateResult(item);
                }
                else
                {
                    item.Group = Repository.GetGroup(item.ParentId);
                    Repository.AddResult(item);
                }
            }
            await Repository.SaveChangesAsync();
            result.Code = ResultCode.Done;
            return new JsonResult(result);
        }

    }
}
