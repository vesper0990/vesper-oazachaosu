using System;
using Repository.Models;
using Repository.Models.Enums;

namespace Repository.Model {
  public interface IResult {
    long Id { get; set; }
    long UserId { get; set; }
    long GroupId { get; set; }
    short Correct { get; set; }
    short Accepted { get; set; }
    short Wrong { get; set; }
    short Invisibilities { get; set; }
    short TimeCount { get; set; }
    TranslationDirection TranslationDirection { get; set; }
    LessonType LessonType { get; set; }
    DateTime DateTime { get; set; }
    State State { get; set; }
  }
}