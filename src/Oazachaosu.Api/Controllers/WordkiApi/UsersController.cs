using Microsoft.AspNetCore.Mvc;
using NLog;
using Oazachaosu.Api.Exceptions;
using Oazachaosu.Api.Services;
using Oazachaosu.Core.Common;
using System;
using System.Threading.Tasks;

namespace Oazachaosu.Api.Controllers
{
    [Route("[controller]")]
    public class UsersController : ApiControllerBase
    {
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();

        public UsersController(IUserService userService) : base(userService)
        {
        }

        [HttpGet("{name}")]
        public async Task<IActionResult> IsExisted(string name)
        {
            bool isExists = await userService.IsExists(name);
            return Json(isExists);
        }

        [HttpGet("{name}/{password}")]
        public async Task<IActionResult> Get(string name, string password)
        {
            UserDTO user = await userService.GetAsync(name, password);
            if (user == null)
            {
                throw new ApiException(ErrorCode.UserNotFound, $"User with name: '{name}' and password: '{password}' is not found.");
            }
            return Json(user);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] UserDTO userDto)
        {
            if (string.IsNullOrEmpty(userDto.Name))
            {
                throw new ApiException(ErrorCode.NameIsEmpty, $"Property: {nameof(userDto.Name)} cannot be empty.");
            }
            if (string.IsNullOrEmpty(userDto.Password))
            {
                throw new ApiException(ErrorCode.PasswordIsEmpty, $"Property: {nameof(userDto.Password)} cannot be empty.");
            }
            if (await userService.IsExists(userDto.Name))
            {
                throw new ApiException(ErrorCode.UserAlreadyExists, $"User with name: {userDto.Name} has already existed");
            }
            try
            {
                await userService.RegisterAsync(userDto.Name, userDto.Password);
            }
            catch (Exception e)
            {
                logger.Error(e, $"Message: {e.Message}, StackTrace: {e.StackTrace}, ExceptionType: {e.GetType()}");
                throw new ApiException(ErrorCode.Undefined, "Internal server exception");
            }
            return await Get(userDto.Name, userDto.Password);
        }

        [HttpPut]
        public async Task<IActionResult> Put([FromBody] UserDTO userDto)
        {
            try
            {
                await userService.UpdateAsync(userDto.ApiKey, userDto.Password, "newpassword");
            }
            catch (Exception e)
            {
                logger.Error(e, $"Message: {e.Message}, StackTrace: {e.StackTrace}, ExceptionType: {e.GetType()}");
                throw new ApiException(ErrorCode.Undefined, "Internal server exception");
            }
            return Json(await Get(userDto.Name, "newpassword"));
        }
    }
}
