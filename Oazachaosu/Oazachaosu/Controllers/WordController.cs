using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Oazachaosu.Models;
using OazachaosuRepository.IRepo;
using OazachaosuRepository.Model;
using PagedList;
using Repository.Models;
using System;
using System.Web.Routing;
using System.Threading.Tasks;

namespace Oazachaosu.Controllers {
  [Authorize]
  public class WordController : MainController {
    const int OnPage = 20;

    private readonly IWordkiRepository _wordkiRepository;
    public readonly UserManager<User> UserManager;

    public WordController(IWordkiRepository wordkiRepository, DbContext dbContext) {
      _wordkiRepository = wordkiRepository;
      UserManager = new UserManager<User>(new UserStore<User>(dbContext));
    }

    //
    // GET: /Word/
    [Route("words/{groupId}/{page}", Name = "WordIndex")]
    public ActionResult Index(long groupId, int? page) {
      Response.Cookies.Add(new HttpCookie("groupId", groupId.ToString()));
      var user = UserManager.FindById(User.Identity.GetUserId());
      int currentPage = page ?? 1;
      Group group = _wordkiRepository.GetGroup(user.LocalId, groupId);
      var model = new GroupPagedViewModel(group) {
        Words = new PagedList<Word>(group.Words, currentPage, OnPage)
      };
      return View(model);
    }

    [Route("words/edit/{wordId}", Name = "WordEdit")]
    public ActionResult Details(long? wordId) {
      if (!wordId.HasValue) {
        return RedirectToAction("Index", "Group");
      }
      var user = UserManager.FindById(User.Identity.GetUserId());
      IWord model = _wordkiRepository.GetWords(user.LocalId).FirstOrDefault(x => x.Id == wordId);
      return View(model);
    }

    [HttpPost]
    [Route("words/edit", Name = "WordEditAction")]
    public async Task<ActionResult> Edit(EditWordViewModel word) {
      int page;
      HttpCookie cookie;
      var user = UserManager.FindById(User.Identity.GetUserId());
      Word dbWord = _wordkiRepository.GetWords(user.LocalId).FirstOrDefault(x => x.Id == word.Id);
      if (dbWord == null) {
        page = 1;
        cookie = Response.Cookies.Get("GroupPage");
        if (cookie != null) {
          Int32.TryParse(cookie.Value, out page);
        }
        return RedirectToAction("Index", "Group", new RouteValueDictionary { { "page", 1 } });
      }
      dbWord.Language1 = word.Language1;
      dbWord.Language2 = word.Language2;
      dbWord.Language1Comment = word.Language1Commnet;
      dbWord.Language2Comment = word.Language2Commnet;
      dbWord.Visible = word.Visible;
      dbWord.State = int.MaxValue;
      _wordkiRepository.UpdateWord(dbWord);
      _wordkiRepository.SaveChanges();

      page = 1;
      cookie = Response.Cookies.Get("GroupPage");
      if (cookie != null) {
        Int32.TryParse(cookie.Value, out page);
      }
      return RedirectToAction("Index", "Group", new RouteValueDictionary { { "page", 1 } });
    }
  }
}