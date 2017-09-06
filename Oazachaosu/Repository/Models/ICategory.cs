namespace Repository.Models {
  public interface ICategory {

    short Id { get; set; }
    string Name { get; set; }
    string Url { get; set; }
  }
}