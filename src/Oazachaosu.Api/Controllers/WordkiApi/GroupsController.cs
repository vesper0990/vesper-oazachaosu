﻿using Microsoft.AspNetCore.Mvc;
using Oazachaosu.Api.Exceptions;
using Oazachaosu.Api.Models;
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
    public class GroupsController : ApiControllerBase
    {

        private readonly IUserService userService;
        private readonly IGroupService groupService;

        public GroupsController(IUserService userService, IGroupService groupService) : base(userService)
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

        [HttpGet("getGroupItems/{apikey}")]
        public async Task<IActionResult> GetGroupItems(string apiKey)
        {
            User user = await userService.GetUserAsync(apiKey);
            if (user == null)
            {
                throw new ApiException(ErrorCode.UserNotFound, $"User with apiKey: {apiKey} is not found.");
            }
            return Json(await groupService.GetGroupItems(user.Id));
        }

        [HttpGet("getGroupDetails/{apiKey}/{groupId}")]
        public async Task<IActionResult> GetGroupDetails(string apiKey, long groupId)
        {
            User user = await userService.GetUserAsync(apiKey);
            if (user == null)
            {
                throw new ApiException(ErrorCode.UserNotFound, $"User with apiKey: {apiKey} is not found.");
            }
            return Json(groupService.GetGroupDetail(user.Id, groupId));
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] PostGroupViewModel datas)
        {
            User user = await CheckIfUserExists(datas.ApiKey);
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

        [HttpPost("AddGroup")]
        public async Task<IActionResult> AddGroup([FromBody] AddGroupViewModel datas)
        {
            User user = await CheckIfUserExists(datas.ApiKey);
            Group newGroup = groupService.Add(datas.Data, user.Id);
            groupService.SaveChanges();
            return Json(newGroup.Id);
        }

        [HttpPost("EditGroup")]
        public async Task<IActionResult> EditGroup([FromBody] EditGroupViewModel datas)
        {
            User user = await CheckIfUserExists(datas.ApiKey);
            groupService.Edit(datas.Data, user.Id);
            groupService.SaveChanges();
            return Ok();
        }
    }
}
