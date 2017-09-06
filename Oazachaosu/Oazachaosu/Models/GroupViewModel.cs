using System.ComponentModel.DataAnnotations;
using OazachaosuRepository.Model;
using System.Collections.Generic;
using Repository.Models.Language;

namespace Oazachaosu.Models {

  public class EditGroupViewModel {

    public long Id { get; set; }

    [Display(Name = "Nazwa grupy")]
    public string Name { get; set; }

    [Display(Name = "Pierwszy język")]
    public LanguageType Language1 { get; set; }

    [Display(Name = "Drugi język")]
    public LanguageType Language2 { get; set; }

  }

  public class UploadedGroup : Group {
    public IList<Word> Words { get; set; }

    public UploadedGroup() {
      Words = new List<Word>();
    }
  }
}