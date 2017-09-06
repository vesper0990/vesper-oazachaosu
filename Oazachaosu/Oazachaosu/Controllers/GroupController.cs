using System;
using System.Data.Entity;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Oazachaosu.Models;
using OazachaosuRepository.IRepo;
using OazachaosuRepository.Model;
using PagedList;
using Repository.Models.Language;
using System.Threading.Tasks;

namespace Oazachaosu.Controllers {
  [Authorize]
  public class GroupController : MainController {

    const int OnPage = 20;

    private readonly IWordkiRepository _wordkiRepository;
    public readonly UserManager<User> UserManager;

    public GroupController(IWordkiRepository wordkiRepository, DbContext dbContext) {
      _wordkiRepository = wordkiRepository;
      UserManager = new UserManager<User>(new UserStore<User>(dbContext));
    }

    //
    // GET: /Group/
    [Route("groups/{page}", Name = "GroupIndex")]
    public ActionResult Index(int? page = 1) {
      if (!page.HasValue) {
        page = 1;
      }
      Response.Cookies.Add(new HttpCookie("GroupPage", page.Value.ToString(CultureInfo.CurrentCulture)));
      var user = UserManager.FindById(User.Identity.GetUserId());
      int currentPage = page ?? 1;
      return View(_wordkiRepository.GetGroups(user.LocalId).OrderBy(x => x.Id).ToPagedList<Group>(currentPage, OnPage));
    }

    [Route("wordki/group/{groupId}", Name = "GroupDetails")]
    public ActionResult Details(long? groupId) {
      if (!groupId.HasValue) {
        return RedirectToAction("Index", "Group");
      }
      var user = UserManager.FindById(User.Identity.GetUserId());
      var group = _wordkiRepository.GetGroup(user.LocalId, groupId.Value);
      ViewData["Languages"] = LanguageFactory.GetLanguages();
      var model = new EditGroupViewModel {
        Id = group.Id,
        Name = group.Name,
        Language1 = group.Language1Type,
        Language2 = group.Language2Type,
      };
      return View(model);
    }

    [HttpPost]
    [Route("group/edit", Name = "GroupEdit")]
    public async Task<ActionResult> Edit(EditGroupViewModel group) {
      int page;
      HttpCookie cookie;
      var user = UserManager.FindById(User.Identity.GetUserId());
      var dbGroup = _wordkiRepository.GetGroup(user.LocalId, group.Id);
      if (dbGroup == null) {
        page = 1;
        cookie = Response.Cookies.Get("GroupPage");
        if (cookie != null) {
          Int32.TryParse(cookie.Value, out page);
        }
        return RedirectToAction("Index", "Group", new RouteValueDictionary { { "page", page } });
      }
      dbGroup.Name = group.Name;
      dbGroup.Language1Type = group.Language1;
      dbGroup.Language2Type = group.Language2;
      await _wordkiRepository.SaveChangesAsync();
      page = 1;
      cookie = Response.Cookies.Get("GroupPage");
      if (cookie != null) {
        Int32.TryParse(cookie.Value, out page);
      }
      return RedirectToAction("Index", "Group", new RouteValueDictionary { { "page", page } });
    }

    [HttpPost]
    [Route("upload/group", Name = "GroupUpload")]
    public ActionResult Upload(HttpPostedFileBase file) {
      var user = UserManager.FindById(User.Identity.GetUserId());
      string fileContent = new System.IO.StreamReader(file.InputStream).ReadToEnd();
      UploadedGroup model = new UploadedGroup();
      model.Name = file.FileName;
      var lines = fileContent.Split('|');
      foreach (var line in lines) {
        var elements = line.Split(';');
        if (elements.Length != 2) {
          continue;
        }
        Word word = new Word {
          Language1 = elements[0],
          Language2 = elements[1],
        };
        model.Words.Add(word);
      }
      Response.Cookies.Add(new HttpCookie("groupName", model.Name));
      Response.Cookies.Add(new HttpCookie("userId", user.LocalId.ToString()));
      return View(model);
    }
  }
}