
namespace Repository.Models {
  public class ArticleComment {
    public int Id { get; set; }
    public int? ArticleId { get; set; }
    public string UserName { get; set; }
    public string Content { get; set; }
    public System.DateTime DateTime { get; set; }
  }
}