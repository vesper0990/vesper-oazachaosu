using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using OazachaosuCore.Exceptions;
using OazachaosuCore.Helpers;
using OazachaosuCore.Helpers.Respone;
using OazachaosuCore.Models.ApiViewModels;
using OazachaosuCore.Services;
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

        private readonly IUserService userService;
        private readonly IGroupService groupService;

        public GroupsController(IUserService userService, IGroupService groupService) : base()
        {
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
            return Json(groupService.GetGroups(user.Id, dateTime));
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] PostGroupViewModel datas)
        {
            if (string.IsNullOrEmpty(datas.ApiKey))
            {
                throw new ApiException(ErrorCode.ApiKeyIsEmpty, $"Parameter: {nameof(datas.ApiKey)} cannot be empty.");
            }
            DateTime now = DateTime.Now;
            User user = await userService.GetUserAsync(datas.ApiKey);
            if (user == null)
            {
                throw new ApiException(ErrorCode.UserNotFound, $"User with apiKey: {datas.ApiKey} is not found.");
            }
            IEnumerable<Group> dbGroups = groupService.GetAll();
            foreach (GroupDTO groupDto in datas.Data)
            {
                if (dbGroups.Any(x => x.Id == groupDto.Id && x.UserId == user.Id))
                {
                    groupService.Update(groupDto, user.Id);
                }
                else
                {
                    groupService.Add(groupDto, user.Id);
                }
            }
            groupService.SaveChanges();
            return Ok();
        }
    }
}
