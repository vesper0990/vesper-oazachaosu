using Repository.Models.Enums;

namespace Repository.Model {
  public interface IWord {
    long Id { get; set; }
    long UserId { get; set; }
    long GroupId { get; set; }
    string Language1 { get; set; }
    string Language2 { get; set; }
    byte Drawer { get; set; }
    string Language1Comment { get; set; }
    string Language2Comment { get; set; }
    bool Visible { get; set; }
    State State { get; set; }
  }
}