using Microsoft.AspNetCore.Mvc;
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
    public class GroupsController : ApiControllerBase
    {
        private IWordkiRepo Repository { get; set; }

        public GroupsController(IWordkiRepo wordkiRepo) : base()
        {
            Repository = wordkiRepo;
        }

        [HttpGet("{dateTime}/{apiKey}")]
        public IActionResult Get(DateTime dateTime, string apiKey)
        {
            User user = Repository.GetUsers().SingleOrDefault(x => x.ApiKey.Equals(apiKey));
            if (user == null)
            {
                return StatusCode((int)HttpStatusCode.Unauthorized);
            }
            return Json(GroupConverter.GetDTOsFromGroups(Repository.GetGroups().Where(x => x.UserId == user.Id && x.LastChange > dateTime)));
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] PostGroupViewModel datas)
        {
            DateTime now = DateTime.Now;
            User user = Repository.GetUsers().SingleOrDefault(x => x.ApiKey.Equals(datas.ApiKey));
            if (user == null)
            {
                return StatusCode((int)HttpStatusCode.Unauthorized);
            }
            IEnumerable<Group> groups = GroupConverter.GetGroupsFromDTOs(datas.Data);
            IQueryable<Group> dbGroups = Repository.GetGroups();
            foreach (Group group in groups)
            {
                group.LastChange = now;
                group.UserId = user.Id;
                if (dbGroups.Any(x => x.Id == group.Id && x.UserId == group.UserId))
                {
                    Repository.UpdateGroup(group);
                }
                else
                {
                    Repository.AddGroup(group);
                }
            }
            await Repository.SaveChangesAsync();
            return Ok();
        }


        [HttpGet("get2")]
        public IActionResult Get2([FromServices] IHeaderElementProvider headerElementProvider)
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
            result.Object = GroupConverter.GetDTOsFromGroups(Repository.GetGroups().Where(x => x.UserId == user.Id && x.LastChange > dateTime));
            result.Code = ResultCode.Done;
            return new JsonResult(result);
        }

        [HttpPost("Post2")]
        public async Task<IActionResult> Post2([FromServices] IBodyProvider bodyProvider, [FromServices] IHeaderElementProvider headerElementProvider)
        {
            DateTime now = DateTime.Now;
            ApiResult result = new ApiResult();
            string apiKey = headerElementProvider.GetElement(Request, "apikey");
            User user = Repository.GetUsers().SingleOrDefault(x => x.ApiKey.Equals(apiKey));
            if (user == null)
            {
                result.Code = ResultCode.UserNotFound;
                result.Message = "User not found.";
                return new JsonResult(result);
            }
            string content = await bodyProvider.GetBodyAsync(Request);
            IEnumerable<GroupDTO> DTOs = JsonConvert.DeserializeObject<IEnumerable<GroupDTO>>(content);
            IEnumerable<Group> groups = GroupConverter.GetGroupsFromDTOs(DTOs);
            IQueryable<Group> dbGroups = Repository.GetGroups();
            foreach (Group group in groups)
            {
                group.LastChange = now;
                group.UserId = user.Id;
                if (dbGroups.Any(x => x.Id == group.Id && x.UserId == group.UserId))
                {
                    Repository.UpdateGroup(group);
                }
                else
                {
                    Repository.AddGroup(group);
                }
            }
            await Repository.SaveChangesAsync();
            result.Code = ResultCode.Done;
            return new JsonResult(result);
        }
    }
}
