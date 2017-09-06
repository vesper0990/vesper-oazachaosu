
namespace Repository.Models {
  public class Word {
    public long Id { get; set; }
    public long UserId { get; set; }
    public long GroupId { get; set; }
    public string Language1 { get; set; }
    public string Language2 { get; set; }
    public sbyte Drawer { get; set; }
    public string Language1Comment { get; set; }
    public string Language2Comment { get; set; }
    public bool Visible { get; set; }
    public sbyte State { get; set; }
    public System.DateTime? LastUpdateTime { get; set; }
  }
}