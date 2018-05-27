using Microsoft.AspNetCore.Mvc;
using Oazachaosu.Api.Exceptions;
using Oazachaosu.Api.Services;
using Oazachaosu.Core;
using Oazachaosu.Core.Common;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Oazachaosu.Api.Controllers
{
    public class ApiControllerBase : Controller
    {

        protected readonly IUserService userService;        

        public ApiControllerBase()
        {

        }

        public ApiControllerBase(IUserService userService)
        {
            this.userService = userService;
        }

        protected async Task<string> GetContnet()
        {
            using (StreamReader reader = new StreamReader(Request.Body, Encoding.UTF8))
            {
                return await reader.ReadToEndAsync();
            }
        }

        protected async Task<User> CheckIfUserExists(string apiKey)
        {
            if (string.IsNullOrEmpty(apiKey))
            {
                throw new ApiException(ErrorCode.ApiKeyIsEmpty, $"Parameter: {nameof(apiKey)} cannot be empty.");
            }
            User user = await userService.GetUserAsync(apiKey);
            if (user == null)
            {
                throw new ApiException(ErrorCode.UserNotFound, $"User with apiKey: {apiKey} is not found.");
            }
            return user;
        }
    }
}
