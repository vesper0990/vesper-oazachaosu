using System.Data.Entity;
using System.Net;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Oazachaosu.Models;
using OazachaosuRepository.IRepo;
using OazachaosuRepository.Model;
using PagedList;

namespace Oazachaosu.Controllers {
  [Authorize]
  public class ResultController : MainController {
    const int OnPage = 20;

    private readonly IWordkiRepository WordkiRepository;
    public readonly UserManager<User> UserManager;

    public ResultController(IWordkiRepository wordkiRepository, DbContext dbContext) {
      WordkiRepository = wordkiRepository;
      UserManager = new UserManager<User>(new UserStore<User>(dbContext));
    }

    //
    // GET: /Word/
    [Route("results/{groupId}/{page}", Name = "ResultIndex")]
    public ActionResult Index(long? groupId, int? page) {
      if (!groupId.HasValue) {
        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
      }
      var user = UserManager.FindById(User.Identity.GetUserId());
      int currentPage = page ?? 1;
      Group group = WordkiRepository.GetGroup(user.LocalId, groupId.Value);
      var model = new GroupPagedViewModel(group) {
        Results = new PagedList<Result>(group.Results, currentPage, OnPage)
      };
      return View(model);
    }
  }
}