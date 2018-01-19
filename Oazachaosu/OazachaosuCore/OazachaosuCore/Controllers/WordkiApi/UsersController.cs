using Microsoft.AspNetCore.Mvc;
using OazachaosuCore.Helpers;
using OazachaosuCore.Helpers.Respone;
using Repository;
using Repository.Model.DTOConverters;
using System;
using System.Linq;
using System.Net;
using WordkiModelCore.DTO;

namespace OazachaosuCore.Controllers
{
    [Route("[controller]")]
    public class UsersController : ApiControllerBase
    {

        private IWordkiRepo Repository { get; set; }

        public UsersController(IWordkiRepo repository)
        {
            Repository = repository;
        }

        [HttpGet("{name}/{password}")]
        public IActionResult Get(string name, string password)
        {
            User user = Repository.GetUsers().SingleOrDefault(x => x.Name.Equals(name));
            if (user == null)
            {
                return StatusCode((int)HttpStatusCode.NotFound);
            }
            if (!user.Password.Equals(password))
            {
                return StatusCode((int)HttpStatusCode.Unauthorized);
            }
            return Json(UserConverter.GetDTOFromModel(user));
        }

        [HttpPost]
        public IActionResult Post([FromBody] UserDTO userDto)
        {
            if (string.IsNullOrEmpty(userDto.Name))
            {
                return StatusCode((int)HttpStatusCode.UnsupportedMediaType, $"Property: {nameof(userDto.Name)} is empty.");
            }
            if (string.IsNullOrEmpty(userDto.Password))
            {
                return StatusCode((int)HttpStatusCode.UnsupportedMediaType, $"Property: {nameof(userDto.Password)} is empty.");
            }
            if (Repository.GetUsers().Any(x => x.Name.Equals(userDto.Name)))
            {
                return StatusCode((int)HttpStatusCode.Found, userDto);
            }
            User user = UserConverter.GetModelFromDTO(userDto);
            user.ApiKey = RandomString(32);
            Repository.AddUser(user);
            Repository.SaveChanges();
            userDto = UserConverter.GetDTOFromModel(Repository.GetUsers().SingleOrDefault(x => x.Name.Equals(userDto.Name)));
            return Json(userDto);
        }

        [HttpPut]
        public IActionResult Put([FromBody] UserDTO userDto)
        {
            User user = Repository.GetUsers().SingleOrDefault(x => x.ApiKey.Equals(userDto.ApiKey));
            if (user == null)
            {
                return StatusCode((int)HttpStatusCode.NotFound);
            }
            user.Password = userDto.Password;
            Repository.UpdateUser(user);
            Repository.SaveChanges();
            user = Repository.GetUsers().SingleOrDefault(x => x.Name.Equals(userDto.Name));
            return Json(UserConverter.GetDTOFromModel(Repository.GetUsers().SingleOrDefault(x => x.Name.Equals(userDto.Name))));
        }

        [HttpGet("Get2")]
        public IActionResult Get2([FromServices] IHeaderElementProvider headerElementProvider)
        {
            ApiResult result = new ApiResult();
            string userName = headerElementProvider.GetElement(Request, "userName");
            string hashPassword = headerElementProvider.GetElement(Request, "password");
            User user = Repository.GetUsers().SingleOrDefault(x => x.Name.Equals(userName));
            if (user == null)
            {
                result.Code = ResultCode.UserNotFound;
                return new JsonResult(result);
            }
            if (!user.Password.Equals(hashPassword))
            {
                result.Code = ResultCode.AuthorizationError;
                return new JsonResult(result);
            }
            result.Code = ResultCode.Done;
            result.Object = UserConverter.GetDTOFromModel(user);
            return new JsonResult(result);
        }

        [HttpPost("Post2")]
        public IActionResult Post2([FromServices] IHeaderElementProvider headerElementProvider)
        {
            ApiResult result = new ApiResult();
            string userName = headerElementProvider.GetElement(Request, "userName");
            string hashPassword = headerElementProvider.GetElement(Request, "password");
            if (Repository.GetUsers().Any(x => x.Name.Equals(userName)))
            {
                result.Code = ResultCode.UserAlreadyExists;
                return new JsonResult(result);
            }
            User user = new User()
            {
                Name = userName,
                Password = hashPassword,
                ApiKey = hashPassword
            };
            Repository.AddUser(user);
            Repository.SaveChanges();
            UserDTO userDto = UserConverter.GetDTOFromModel(Repository.GetUsers().SingleOrDefault(x => x.Name.Equals(userName)));
            result.Object = userDto;
            result.Code = ResultCode.Done;
            return new JsonResult(result);
        }

        private static Random random = new Random();
        public static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        //[HttpPut("Put2")]
        //public IActionResult Update2([FromServices] IHeaderElementProvider headerElementProvider)
        //{
        //    ApiResult result = new ApiResult();
        //    string userName = headerElementProvider.GetElement(Request, "userName");
        //    string hashPassword = headerElementProvider.GetElement(Request, "password");
        //    string newHashPassword = headerElementProvider.GetElement(Request, "newPassword");
        //    User user = Repository.GetUsers().SingleOrDefault(x => x.Name.Equals(userName) && x.Password.Equals(hashPassword));
        //    if (user == null)
        //    {
        //        result.Code = ResultCode.UserNotFound;
        //        return new JsonResult(result);
        //    }

        //    user.Password = newHashPassword;
        //    user.ApiKey = newHashPassword;
        //    Repository.UpdateUser(user);
        //    Repository.SaveChanges();
        //    user = Repository.GetUsers().SingleOrDefault(x => x.Name.Equals(userName));
        //    result.Object = UserConverter.GetDTOFromModel(user);
        //    result.Code = ResultCode.Done;
        //    return new JsonResult(result);
        //}
    }
}
