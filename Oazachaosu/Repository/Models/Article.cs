using System.Collections.Generic;

namespace Repository.Models {
  public class Article {

    public Article() {
      ArticleCategory = new HashSet<Article_Category>();
    }

    public int Id { get; set; }
    public string Title { get; set; }
    public string Abstract { get; set; }
    public string ContentUrl { get; set; }
    public System.DateTime? DateTime { get; set; }

    public ICollection<Article_Category> ArticleCategory { get; set; }

  }
}