using System.Collections.Generic;

namespace Repository.Model {
  public interface ICategory {

    short Id { get; set; }
    string Name { get; set; }
    string Url { get; set; }

    ICollection<IArticleCategory> ArticleCategories { get; set; }
  }
}