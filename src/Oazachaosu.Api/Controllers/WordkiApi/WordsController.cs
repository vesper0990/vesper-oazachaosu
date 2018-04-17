using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Oazachaosu.Api.Exceptions;
using Oazachaosu.Api.Models.ApiViewModels;
using Oazachaosu.Api.Services;
using Oazachaosu.Core;
using Oazachaosu.Core.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Oazachaosu.Api.Controllers
{
    [Route("[controller]")]
    public class WordsController : ApiControllerBase
    {

        private readonly IWordService wordService;
        private readonly IUserService userService;
        private readonly IGroupService groupService;

        public WordsController(IWordService wordService, IGroupService groupService,
            IUserService userService) : base()
        {
            this.wordService = wordService;
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
            return Json(wordService.Get(user.Id, dateTime));
        }

        [HttpGet("{dateTime}/{groupId}/{apiKey}")]
        public async Task<IActionResult> Get(long groupId, string apiKey)
        {
            User user = await userService.GetUserAsync(apiKey);
            if (user == null)
            {
                throw new ApiException(ErrorCode.UserNotFound, $"User with apiKey: {apiKey} is not found.");
            }
            return Json(wordService.Get(user.Id, groupId));
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] PostWordsViewModel data)
        {
            User user = await userService.GetUserAsync(data.ApiKey);
            if (user == null)
            {
                throw new ApiException(ErrorCode.UserNotFound, $"User with apiKey: {data.ApiKey} is not found.");
            }
            IEnumerable<Group> dbGroups = groupService.GetGroups(user.Id).Include(x => x.Words);
            foreach (var word in data.Data)
            {
                Group group = dbGroups.SingleOrDefault(x => x.Id == word.GroupId);
                if (group == null)
                {
                    continue;
                }
                if (group.Words.Any(x => x.Id == word.Id))
                {
                    wordService.Update(word, user.Id);
                }
                else
                {
                    wordService.Add(word, user.Id);
                }
            }
            wordService.SaveChanges();
            return Ok();
        }
    }
}
