using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using Newtonsoft.Json;
using Repository.Models;

namespace OazachaosuRepository.Model {
  using System;
  using Repository.Models.Language;

  public class Group : IGroup {

    public Group(Group group)
      : this() {
      Id = group.Id;
      UserId = group.UserId;
      Name = group.Name;
      Language1 = group.Language1;
      Language2 = group.Language2;
      State = group.State;
      LastUpdateTime = group.LastUpdateTime;
      Words = group.Words;
      Results = group.Results;
    }

    public Group() {
      Language1 = LanguageFactory.GetLanguage(LanguageType.Default);
      Language2 = LanguageFactory.GetLanguage(LanguageType.Default);
      Words = new HashSet<Word>();
      Results = new HashSet<Result>();
    }

    [Key, Column(Order = 0)]
    public long Id { get; set; }
    [Key, Column(Order = 1)]
    public long UserId { get; set; }
    [MaxLength(64)]
    public string Name { get; set; }

    [JsonIgnore]
    [Column("Language1")]
    public LanguageType Language1Type {
      get { return Language1 == null ? LanguageType.Default : Language1.Type;  }
      set {
        Language1 = LanguageFactory.GetLanguage(value);
      }
    }

    [NotMapped]
    public ILanguage Language1 { get; set; }

    [JsonIgnore]
    [Column("Language2")]
    public LanguageType Language2Type {
      get { return Language2 == null ? LanguageType.Default : Language2.Type; }
      set {
        Language2 = LanguageFactory.GetLanguage(value);
      }
    }

    [NotMapped]
    public ILanguage Language2 { get; set; }

    public int State { get; set; }

    [JsonIgnore]
    public DateTime? LastUpdateTime { get; set; }
    [JsonIgnore]
    public virtual ICollection<Word> Words { get; set; }
    [JsonIgnore]
    public virtual ICollection<Result> Results { get; set; }

    public void UpdateElement2(IGroup group) {
      int state = group.State;
      var currentProperies = GetType().GetProperties();
      var newProperties = group.GetType().GetProperties();

      for (int i = 0; i < newProperties.Length; i++) {
        bool needUpdate = (state & (1 << i)) > 0;
        if (!needUpdate) {
          continue; 
        }
        currentProperies.Single(x => x.Name.Equals(newProperties[i].Name)).SetValue(this, newProperties[i].GetValue(group));
      }
      State = group.State;
      LastUpdateTime = DateTime.Now;
    }

    public void UpdateElement(IGroup group) {
      int index = 0;
      if (CheckUpdate(group.State, index++)) {
        Id = group.Id;
      }

      if (CheckUpdate(group.State, index++)) {
        UserId = group.UserId;
      }

      if (CheckUpdate(group.State, index++)) {
        Name = group.Name;
      }

      if (CheckUpdate(group.State, index++)) {
        Language1 = group.Language1;
      }

      if (CheckUpdate(group.State, index)) {
        Language2 = group.Language2;
      }

      State = group.State;
      LastUpdateTime = DateTime.Now;
    }

    private bool CheckUpdate(int state, int index) {
      return (state & (1 << index)) > 0;
    }
  }
}
