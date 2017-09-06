using System.Data.Entity;
using System.Linq;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Oazachaosu.Controllers.Api.Response;
using OazachaosuRepository.IRepo;
using OazachaosuRepository.Model;

namespace Oazachaosu.Controllers.Api {
  public abstract class MainApiController : MainController {

    private const string AuthorizationHeader = "authorization";

    protected long UserId { get; set; }
    protected string PostString { get; set; }
    protected IApiResponse ApiResponse { get; set; }

    protected readonly IWordkiRepository WordkiRepository;
    protected readonly UserManager<User> UserManager;

    protected MainApiController(IWordkiRepository wordkiRepository, DbContext dbContext) {
      WordkiRepository = wordkiRepository;
      UserManager = new UserManager<User>(new UserStore<User>(dbContext));
      ApiResponse = new PlainApiResponse() {
        IsError = true,
        Message = "",
      };
    }

    protected bool CheckAuthorization() {
      string apiKey = Request.Headers[AuthorizationHeader];
      User user = UserManager.Users.SingleOrDefault(x => x.ApiKey.Equals(apiKey));
      if (user == null) {
        ApiResponse.Message += "Authorization Error | ";
        return false;
      }
      UserId = user.LocalId;
      return true;
    }

    protected bool CheckPostValue() {
      PostString = new System.IO.StreamReader(Request.InputStream).ReadToEnd();
      bool result = PostString != null;
      if (!result) {
        ApiResponse.Message += "Message Error | ";
      }
      return result;
    }
  }
}