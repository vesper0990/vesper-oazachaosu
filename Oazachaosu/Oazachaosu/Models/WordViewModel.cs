using OazachaosuRepository.Model;
using PagedList;

namespace Oazachaosu.Models {
  public class GroupPagedViewModel : Group {

    public GroupPagedViewModel() { }

    public GroupPagedViewModel(Group group)
      : base(group) {
    }

    public new IPagedList<Word> Words { get; set; }
    public new IPagedList<Result> Results { get; set; } 
  }

  public class EditWordViewModel {
    public long Id { get; set; }
    public long GroupId { get; set; }
    public string Language1 { get; set; }
    public string Language2 { get; set; }
    public string Language1Commnet { get; set; }
    public string Language2Commnet { get; set; }
    public bool Visible { get; set; }
  }
}