namespace OazachaosuRepository.Model {
  using System.Collections.Generic;
  using System.ComponentModel.DataAnnotations;

  public class Category {

    public Category() {
    }

    public short Id { get; set; }
    [MaxLength(64)]
    [Display(Name = "Nazwa kategorii")]
    public string Name { get; set; }
    [MaxLength(128)]
    [Display(Name = "Œcie¿ka do kategorii")]
    public string Url { get; set; }

    public virtual ICollection<Article> Articles { get; set; }
  }
}
