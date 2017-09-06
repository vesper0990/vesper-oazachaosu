using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using OazachaosuRepository.Model;

namespace Oazachaosu.Models {
  public class ArticleViewModel {
    public Article Article { get; set; }

    [Display(Name = "Kategorie")]
    public List<Category> Categories { get; set; }

    public ArticleViewModel() {
      Categories = new List<Category>();
    }
  }

  public class CategoryToSelect {
    [Display(Name = "Kategoria")]
    public short SelectedCategory { get; set; }
    public long ArticleId { get; set; }
    public IQueryable<System.Web.Mvc.SelectListItem> Categories { get; set; }
  }

  public class CategroyActionFormViewModel {

    public short SelectedCategory { get; set; }
    public string Action { get; set; }
    public int ArticleId { get; set; }

  }

  public class CategoryIndexViewModel {
    public Category CategoryMain { get; set; }
    public int Count { get; set; }
  }

  public class CategoryViewModel {
    public Category CategoryMain { get; set; }
    public IList<ArticleViewModel> ArticleList { get; set; }

    public CategoryViewModel() {
      ArticleList = new List<ArticleViewModel>();
    }
  }
}