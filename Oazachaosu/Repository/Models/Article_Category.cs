
namespace Repository.Models {
  public class Article_Category {

    public int Id { get; set; }
    public int ArticleId { get; set; }
    public short CategoryId { get; set; }

    public virtual Article Article { get; set; }
    public virtual Category Category { get; set; }

  }
}