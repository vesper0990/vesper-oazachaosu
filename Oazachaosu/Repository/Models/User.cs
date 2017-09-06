using System;

namespace Repository.Models {
  public class User {

    public long Id { get; set; }
    public string Name { get; set; }
    public string Password { get; set; }
    public string ApiKey { get; set; }
    public bool IsAdmin { get; set; }
    public DateTime CreateDate { get; set; }
    public DateTime LastLoginDate { get; set; }
    public sbyte State { get; set; }

  }
}