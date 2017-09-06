namespace Repository.Models {
  public interface IArticleComment {

    int Id { get; set; }
    int ArticleId { get; set; }
    string UserName { get; set; }
    string Content { get; set; }
    System.DateTime DateTime { get; set; }

  }
}