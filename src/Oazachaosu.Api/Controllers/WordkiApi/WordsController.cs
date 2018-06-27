using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Oazachaosu.Api.Exceptions;
using Oazachaosu.Api.Models.ApiViewModels;
using Oazachaosu.Api.Services;
using Oazachaosu.Core;
using Oazachaosu.Core.Common;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Oazachaosu.Api.Controllers
{
    [Route("[controller]")]
    public class WordsController : ApiControllerBase
    {

        private readonly IWordService wordService;
        private readonly IGroupService groupService;

        public WordsController(IWordService wordService, IGroupService groupService,
            IUserService userService) : base(userService)
        {
            this.wordService = wordService;
            this.groupService = groupService;
        }

        [HttpGet("{dateTime}/{apiKey}")]
        public async Task<IActionResult> Get(DateTime dateTime, string apiKey)
        {
            User user = await CheckIfUserExists(apiKey);
            return Json(wordService.Get(user.Id, dateTime));
        }

        [HttpGet("{dateTime}/{groupId}/{apiKey}")]
        public async Task<IActionResult> Get(long groupId, string apiKey)
        {
            User user = await CheckIfUserExists(apiKey);
            return Json(wordService.Get(user.Id, groupId));
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] PostWordsViewModel data)
        {
            User user = await CheckIfUserExists(data.ApiKey);
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

        [HttpPost("AddWord")]
        public async Task<IActionResult> AddWord([FromBody] AddWordViewModel viewModel)
        {
            User user = await CheckIfUserExists(viewModel.ApiKey);
            long wordId = wordService.Add(viewModel.Data, user.Id);
            return Json(wordId);
        }

        [HttpPost("UpdateWord")]
        public async Task<IActionResult> UpdateWord([FromBody] UpdateWordViewModel viewModel)
        {
            User user = await CheckIfUserExists(viewModel.ApiKey);
            wordService.Update(viewModel.Data, user.Id);
            wordService.SaveChanges();
            return Ok();
        }
    }
}
