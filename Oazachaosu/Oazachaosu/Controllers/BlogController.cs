using System;
using System.Linq;
using System.Web.Mvc;
using Oazachaosu.Models;
using OazachaosuRepository.IRepo;
using OazachaosuRepository.Model;
using System.Net;

namespace Oazachaosu.Controllers {
  [Authorize]
  public class BlogController : Controller {

    private readonly IBlogRepository BlogRepository;

    public BlogController(IBlogRepository articleRepository) {
      BlogRepository = articleRepository;
    }

    [Route("", Name = "BlogIndex2")]
    [Route("blog", Name = "BlogIndex")]
    [AllowAnonymous]
    public ActionResult Index() {
      return View(BlogRepository.GetArticles());
    }

    [Route("blog/{url}", Name = "BlogDetails")]
    [AllowAnonymous]
    public ActionResult Details(string url) {
      if (string.IsNullOrEmpty(url)) {
        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
      }
      return View(BlogRepository.GetArticleByUrl(url));
    }

    [Route("blog/menu", Name = "BlogMenu")]
    [AllowAnonymous]
    public PartialViewResult Menu() {
      return PartialView("_LeftMenuPartial", BlogRepository.GetCategories());
    }

    [Authorize(Users = "admin")]
    [Route("blog/edit/{id}", Name = "BlogEdit", Order = 1)]
    public ActionResult Edit(long? id) {
      if (!id.HasValue) {
        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
      }
      return View(BlogRepository.GetArticleById(id.Value));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    [Route("blog/edit/{id}", Name = "BlogEditAction", Order = 2)]
    [Authorize(Users = "admin")]
    public ActionResult Edit(Article article, long? id) {
      if (article == null || !id.HasValue) {
        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
      }
      BlogRepository.UpdateArticle(article);
      BlogRepository.SaveChanges();
      return RedirectToAction("Edit", new { id = id.Value });
    }

    [HttpGet]
    [Route("blog/addCategory/{articleId}", Name = "BlogAddCategory")]
    [Authorize(Users = "admin")]
    public ActionResult AddCategory(long? articleId) {
      if (!articleId.HasValue) {
        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
      }
      var model = new CategoryToSelect {
        Categories = BlogRepository.GetCategories().Select(x => new System.Web.Mvc.SelectListItem() {
          Text = x.Name,
          Value = x.Id.ToString()
        }),
        ArticleId = articleId.Value,
      };
      return PartialView("_AddCategoryPartial", model);
    }

    [HttpPost]
    [Route("blog/addCategoryAction", Name = "BlogAddCategoryAction")]
    [Authorize(Users = "admin")]
    public ActionResult AddCategoryAction(CategroyActionFormViewModel formModel) {
      if (formModel == null) {
        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
      }
      BlogRepository.AddCategoryToArticle(formModel.ArticleId, formModel.SelectedCategory);
      try {
        BlogRepository.SaveChanges();
      } catch (Exception e) {

      }
      return RedirectToAction("Edit", new { Id = formModel.ArticleId });
    }

    [HttpGet]
    [Route("blog/removeCategory/{articleId}/{categoryId}", Name = "BlogRemoveCategory")]
    [Authorize(Users = "admin")]
    public ActionResult RemoveCategory(long? articleId, long? categoryId) {
      if (!articleId.HasValue || !categoryId.HasValue) {
        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
      }
      BlogRepository.RemoveCategoryFromArticle(articleId.Value, categoryId.Value);
      try {
        BlogRepository.SaveChanges();
      } catch (Exception e) {

      }
      return RedirectToAction("Edit", new { Id = articleId.Value });
    }
  }
}