using System.ComponentModel.DataAnnotations;
using Repository.Models;

namespace OazachaosuRepository.Model {
  using System;

  public class ArticleComment : IArticleComment {
    public int Id { get; set; }
    public int ArticleId { get; set; }
    [MaxLength(32)]
    public string UserName { get; set; }
    [MaxLength(512)]
    public string Content { get; set; }
    public DateTime DateTime { get; set; }
  }
}
