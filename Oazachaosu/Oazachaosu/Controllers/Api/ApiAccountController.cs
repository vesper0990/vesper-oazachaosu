using System;
using System.Data.Entity;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Newtonsoft.Json;
using OazachaosuRepository.IRepo;
using OazachaosuRepository.Model;

namespace Oazachaosu.Controllers.Api {
  public class ApiAccountController : MainApiController {

    public ApiAccountController(IWordkiRepository wordkiRepository, DbContext dbContext)
      : base(wordkiRepository, dbContext) {
    }

    [HttpPost]
    [Route("api/login")]
    public async Task<ActionResult> Login() {
      if (!CheckPostValue()) {
        return new ApiJsonResult { Data = ApiResponse };
      }
      User user = JsonConvert.DeserializeObject<User>(PostString);
      string hashPassword = OazachaosuRepository.Model.Hash.GetMd5Hash(MD5.Create(), user.Password);
      var temp = await UserManager.FindAsync(user.Name, user.Password);
      User dbUser = UserManager.Users.FirstOrDefault(x => x.Name.Equals(user.Name) && x.Password.Equals(hashPassword));
      ApiResponse.IsError = dbUser == null;
      ApiResponse.Message = JsonConvert.SerializeObject(dbUser);
      if (dbUser != null) {
        dbUser.LastLoginDateTime = DateTime.Now;
        UserManager.Update(dbUser);
      }
      return new ApiJsonResult { Data = ApiResponse };
    }

    [HttpPost]
    [Route("api/register")]
    public async Task<ActionResult> Register() {
      if (!CheckPostValue()) {
        return new ApiJsonResult { Data = ApiResponse };
      }
      User user = JsonConvert.DeserializeObject<User>(PostString);
      bool userExist = UserManager.Users.Any(x => x.Name.Equals(user.Name));
      if (userExist) {
        ApiResponse.IsError = true;
        ApiResponse.Message = "Użytkownik istnieje";
        return new ApiJsonResult { Data = ApiResponse };
      }
      string hashPassword = OazachaosuRepository.Model.Hash.GetMd5Hash(MD5.Create(), user.Password);
      string password = user.Password;
      user.LocalId = DateTime.Now.Ticks;
      user.ApiKey = hashPassword;
      user.Password = hashPassword;
      user.UserName = user.Name;
      user.CreateDateTime = DateTime.Now;
      user.LastLoginDateTime = DateTime.Now;
      var result = await UserManager.CreateAsync(user, password);
      if (result.Succeeded) {
        ApiResponse.IsError = false;
        ApiResponse.Message = JsonConvert.SerializeObject(user);
      } else {
        ApiResponse.Message = result.Errors.First();
      }
      return new ApiJsonResult { Data = ApiResponse };
    }

    [HttpGet]
    [Route("api/date")]
    public ActionResult Date() {
      ApiResponse.IsError = false;
      ApiResponse.Message = DateTime.Now.ToString();
      return new ApiJsonResult { Data = ApiResponse };
    }
  }
}