
namespace Repository.Models {
  public class Result {
    public long Id { get; set; }
    public long UserId { get; set; }
    public long GroupId { get; set; }
    public int Correct { get; set; }
    public int Accepted { get; set; }
    public int Wrong { get; set; }
    public int Invisibilities { get; set; }
    public int TimeCount { get; set; }
    public sbyte TranslationDirection { get; set; }
    public sbyte LessonType { get; set; }
    public System.DateTime DateTime { get; set; }
    public sbyte State { get; set; }
    public System.DateTime? LastUpdateTime { get; set; }
  }
}