namespace OazachaosuRepository.Model {
  using System;
  using System.Collections.Generic;
  using System.ComponentModel.DataAnnotations;

  public class Article {

    public int Id { get; set; }
    [MaxLength(64)]
    [Display(Name = "Tytu� artyku�u")]
    public string Title { get; set; }
    [MaxLength(512)]
    [Display(Name = "Abstrakt artyku�u")]
    [DataType(DataType.MultilineText)]
    public string Abstract { get; set; }
    [MaxLength(64)]
    [Display(Name = "�cie�ka do pliku")]
    public string ContentUrl { get; set; }
    public DateTime DateTime { get; set; }

    public virtual ICollection<Category> Categories { get; set; }

  }
}
