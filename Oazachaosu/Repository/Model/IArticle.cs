using System;
using System.Collections.Generic;

namespace Repository.Model {
  public interface IArticle {

    int Id { get; set; }
    string Title { get; set; }
    string Abstract { get; set; }
    string ContentUrl { get; set; }
    DateTime DateTime { get; set; }

    ICollection<IArticleCategory> ArticleCategories { get; set; }

  }
}