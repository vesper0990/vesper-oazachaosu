using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;
using Repository.Models;
using Repository.Models.Enums;

namespace OazachaosuRepository.Model {
  using System;

  public class Result : IResult {
    [Key, Column(Order = 0)]
    public long Id { get; set; }
    [Key, Column(Order = 1)]
    public long UserId { get; set; }
    public long GroupId { get; set; }
    public short Correct { get; set; }
    public short Accepted { get; set; }
    public short Wrong { get; set; }
    public short Invisibilities { get; set; }
    public short TimeCount { get; set; }
    public TranslationDirection TranslationDirection { get; set; }
    public LessonType LessonType { get; set; }
    public DateTime DateTime { get; set; }
    public int State { get; set; }

    [JsonIgnore]
    public DateTime? LastUpdateTime { get; set; }

    public void UpdateElement(IResult result) {
      int index = 0;
      if (CheckUpdate(result.State, index++)) {
        Id = result.Id;
      }

      if (CheckUpdate(result.State, index++)) {
        UserId = result.UserId;
      }

      if (CheckUpdate(result.State, index++)) {
        GroupId = result.GroupId;
      }

      if (CheckUpdate(result.State, index++)) {
        Correct = result.Correct;
      }

      if (CheckUpdate(result.State, index++)) {
        Accepted = result.Accepted;
      }

      if (CheckUpdate(result.State, index++)) {
        Wrong = result.Wrong;
      }

      if (CheckUpdate(result.State, index++)) {
        Invisibilities = result.Invisibilities;
      }

      if (CheckUpdate(result.State, index++)) {
        TimeCount = result.TimeCount;
      }

      if (CheckUpdate(result.State, index++)) {
        TranslationDirection = result.TranslationDirection;
      }

      if (CheckUpdate(result.State, index++)) {
        LessonType = result.LessonType;
      }

      if (CheckUpdate(result.State, index)) {
        DateTime = result.DateTime;
      }

      State = result.State;
      LastUpdateTime = DateTime.Now;
    }

    private bool CheckUpdate(int state, int index) {
      return (state & (1 << index)) > 0;
    }
  }
}
