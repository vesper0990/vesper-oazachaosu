using System.Collections.Generic;

namespace Repository.Models {
  public class Category {
    public Category() {
      ArticleCategories = new HashSet<Article_Category>();
    }

    public short Id { get; set; }
    public string Name { get; set; }
    public string Url { get; set; }

    public ICollection<Article_Category> ArticleCategories { get; set; }
  }
}