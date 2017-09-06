
using System.Collections.Generic;

namespace Repository.Models {
  public class Group {

    public long Id { get; set; }
    public long UserId { get; set; }
    public string Name { get; set; }
    public sbyte Language1Type { get; set; }
    public sbyte Language2Type { get; set; }
    public sbyte State { get; set; }
    public System.DateTime? LastUpdateTime { get; set; }

    public virtual ICollection<Word> Words { get; set; }
    public virtual ICollection<Result> Resutls { get; set; }

  }
}