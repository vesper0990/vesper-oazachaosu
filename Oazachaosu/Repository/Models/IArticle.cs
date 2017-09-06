using System;

namespace Repository.Models {
  public interface IArticle {

    int Id { get; set; }
    string Title { get; set; }
    string Abstract { get; set; }
    string ContentUrl { get; set; }
    DateTime DateTime { get; set; }
  }
}