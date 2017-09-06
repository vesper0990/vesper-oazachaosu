namespace OazachaosuRepository.Model {
  using System;
  using System.Collections.Generic;
  using System.ComponentModel.DataAnnotations;

  public class Article {

    public int Id { get; set; }
    [MaxLength(64)]
    [Display(Name = "Tytu³ artyku³u")]
    public string Title { get; set; }
    [MaxLength(512)]
    [Display(Name = "Abstrakt artyku³u")]
    [DataType(DataType.MultilineText)]
    public string Abstract { get; set; }
    [MaxLength(64)]
    [Display(Name = "Œcie¿ka do pliku")]
    public string ContentUrl { get; set; }
    public DateTime DateTime { get; set; }

    public virtual ICollection<Category> Categories { get; set; }

  }
}
