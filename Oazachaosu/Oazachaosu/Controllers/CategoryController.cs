using System.Web.Mvc;
using OazachaosuRepository.IRepo;

namespace Oazachaosu.Controllers {
  public class CategoryController : Controller {


    private readonly IBlogRepository BlogRepository;

    public CategoryController(IBlogRepository blogrepository) {
      BlogRepository = blogrepository;
    }

    //
    // GET: /Category/
    public ActionResult Index() {
      return View(BlogRepository.GetCategories());
    }

    [Route("Category/{url}", Name = "CategoryDetails")]
    public ActionResult Details(string url) {
      return View(BlogRepository.GetCategoryByUrl(url));
    }



  }
}