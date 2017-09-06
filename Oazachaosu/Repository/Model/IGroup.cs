using Repository.Models.Enums;

namespace Repository.Model {
  
  public interface IGroup {

    long Id { get; set; }
    long UserId { get; set; }
    string Name { get; set; }
    Language Language1 { get; set; }
    Language Language2 { get; set; }
    State State { get; set; }

  }
}