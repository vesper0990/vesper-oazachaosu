using System;
using System.Security.Cryptography;
using System.Text;
using System.Web.Mvc;
using OazachaosuRepository.Model;

namespace Oazachaosu.Controllers {
  public abstract class MainController : Controller {

    protected const string UserTag = "user";

    //protected new User User { get; set; }

    protected bool RequireAuthorization() {
      //User = (User)Session[UserTag];
      return User != null;
    }

    protected ActionResult RedirectToLogin() {
      return RedirectToAction("Login", "Account");
    }
  }

  public class Hash {
    public static string GetMd5Hash(MD5 md5Hash, string input) {
      StringBuilder lBuilder = new StringBuilder();
      byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));
      foreach (byte t in data) {
        lBuilder.Append(t.ToString("x2"));
      }
      return lBuilder.ToString();
    }
  }
}