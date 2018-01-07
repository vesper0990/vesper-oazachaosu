using Microsoft.AspNetCore.Mvc;
using OazachaosuCore.Helpers;
using OazachaosuCore.Helpers.Respone;
using Repository;
using System.Linq;

namespace OazachaosuCore.Controllers
{
    public class UserController : ApiControllerBase
    {

        private IWordkiRepo Repository { get; set; }

        public UserController(IWordkiRepo repository)
        {
            Repository = repository;
        }

        public IActionResult Get([FromServices] IHeaderElementProvider headerElementProvider)
        {
            ApiResult result = new ApiResult();
            string userName = headerElementProvider.GetElement(Request, "userName");
            string hashPassword = headerElementProvider.GetElement(Request, "password");
            User user = Repository.GetUsers().SingleOrDefault(x => x.Name.Equals(userName));
            if(user == null)
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
            result.Object = user;
            return new JsonResult(result);
        }

        public IActionResult Post([FromServices] IHeaderElementProvider headerElementProvider)
        {
            ApiResult result = new ApiResult();
            string userName = headerElementProvider.GetElement(Request, "userName");
            string hashPassword = headerElementProvider.GetElement(Request, "password");
            if(Repository.GetUsers().Count(x => x.Name.Equals(userName)) > 0)
            {
                result.Code = ResultCode.UserNotFound;
                return new JsonResult(result);
            }
            User user = new User()
            {
                Name = userName,
                Password = hashPassword,
                ApiKey = hashPassword
            };
            Repository.AddUser(user);
            user = Repository.GetUsers().SingleOrDefault(x => x.Name.Equals(userName));
            result.Object = user;
            result.Code = ResultCode.Done;
            return new JsonResult(result);
        }
    }
}
