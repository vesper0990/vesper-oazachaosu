using OazachaosuRepository.Model;
using PagedList;

namespace Oazachaosu.Models {
  public class ExtendedResult {
    public Group Group { get; set; }
    public IPagedList<Result> Results { get; set; }
  }
}