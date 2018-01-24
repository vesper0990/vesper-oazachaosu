using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Oazachaosu.Api.Exceptions;
using Oazachaosu.Api.Models.ApiViewModels;
using Oazachaosu.Api.Services;
using Oazachaosu.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Oazachaosu.Api.Controllers
{
    [Route("[controller]")]
    public class ResultsController : ApiControllerBase
    {
        private readonly IResultService resultService;
        private readonly IUserService userService;
        private readonly IGroupService groupService;

        public ResultsController(IResultService resultService, IGroupService groupService,
            IUserService userService) : base()
        {
            this.resultService = resultService;
            this.userService = userService;
            this.groupService = groupService;
        }

        [HttpGet("{dateTime}/{apiKey}")]
        public async Task<IActionResult> Get(DateTime dateTime, string apiKey)
        {
            User user = await userService.GetUserAsync(apiKey);
            if (user == null)
            {
                throw new ApiException(ErrorCode.UserNotFound, $"User with apiKey: {apiKey} is not found.");
            }
            return Json(resultService.Get(user.Id, dateTime));
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] PostResultsViewModel data)
        {
            User user = await userService.GetUserAsync(data.ApiKey);
            if (user == null)
            {
                throw new ApiException(ErrorCode.UserNotFound, $"User with apiKey: {data.ApiKey} is not found.");
            }
            IEnumerable<Group> dbGroups = groupService.GetGroups(user.Id).Include(x => x.Words);
            foreach (var result in data.Data)
            {
                Group group = dbGroups.SingleOrDefault(x => x.Id == result.GroupId);
                if (group == null)
                {
                    continue;
                }
                if (group.Results.Any(x => x.Id == result.Id))
                {
                    resultService.Update(result, user.Id);
                }
                else
                {
                    resultService.Add(result, user.Id);
                }
            }
            resultService.SaveChanges();
            return Ok();
        }
    }
}
