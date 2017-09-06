using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;
using Repository.Models;

namespace OazachaosuRepository.Model {
  using System;
  public class Word : IWord {
    [Key, Column(Order = 0)]
    public long Id { get; set; }

    [Key, Column(Order = 1)]
    public long UserId { get; set; }

    public long GroupId { get; set; }

    [MaxLength(128)]
    [Display(Name = "Słowo w pierwszym języku")]
    public string Language1 { get; set; }

    [MaxLength(128)]
    [Display(Name = "Słowo w drugim języku")]
    public string Language2 { get; set; }

    [Display(Name = "Szuflada")]
    public byte Drawer { get; set; }

    [MaxLength(128)]
    [Display(Name = "Komentarz w pierwszym języku")]
    public string Language1Comment { get; set; }

    [MaxLength(128)]
    [Display(Name = "Komentarz w drugim języku")]
    public string Language2Comment { get; set; }

    [Display(Name = "Widoczmość")]
    public bool Visible { get; set; }

    public int State { get; set; }

    [JsonIgnore]
    public DateTime? LastUpdateTime { get; set; }

    public void UpdateElement(IWord word) {
      int index = 0;
      if (CheckUpdate(word.State, index++)) {
        Id = word.Id;
      }

      if (CheckUpdate(word.State, index++)) {
        UserId = word.UserId;
      }

      if (CheckUpdate(word.State, index++)) {
        GroupId = word.GroupId;
      }

      if (CheckUpdate(word.State, index++)) {
        Language1 = word.Language1;
      }

      if (CheckUpdate(word.State, index++)) {
        Language2 = word.Language2;
      }

      if (CheckUpdate(word.State, index++)) {
        Language1Comment = word.Language1Comment;
      }

      if (CheckUpdate(word.State, index++)) {
        Language2Comment = word.Language2Comment;
      }

      if (CheckUpdate(word.State, index++)) {
        Drawer = word.Drawer;
      }

      if (CheckUpdate(word.State, index)) {
        Visible = word.Visible;
      }

      State = word.State;
      LastUpdateTime = DateTime.Now;
    }

    private bool CheckUpdate(int state, int index) {
      return (state & (1 << index)) > 0;
    }
  }
}
