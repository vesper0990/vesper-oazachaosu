namespace Repository.Models {
  public interface IArticleCategory {

    int Id { get; set; }
    int ArticleId { get; set; }
    short CategoryId { get; set; }

    IArticle Article { get; set; }
    ICategory Category { get; set; }

  }
}