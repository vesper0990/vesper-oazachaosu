using System.ComponentModel;
using Newtonsoft.Json;
using Repository.Helpers;

namespace Repository.Models {
  public interface IWord {
    [JsonProperty(PropertyName = "Id")]
    [PropertyIndex(0)]
    long Id { get; set; }

    [JsonProperty(PropertyName = "UserId")]
    [JsonIgnore]
    [PropertyIndex(1)]
    long UserId { get; set; }

    [JsonProperty(PropertyName = "GroupId")]
    [PropertyIndex(2)]
    long GroupId { get; set; }

    [JsonProperty(PropertyName = "Language1", DefaultValueHandling = DefaultValueHandling.Populate)]
    [PropertyIndex(3)]
    string Language1 { get; set; }

    [JsonProperty(PropertyName = "Language2", DefaultValueHandling = DefaultValueHandling.Populate)]
    [PropertyIndex(4)]
    string Language2 { get; set; }

    [JsonProperty(PropertyName = "Language1Comment", DefaultValueHandling = DefaultValueHandling.Populate)]
    [PropertyIndex(5)]
    string Language1Comment { get; set; }

    [JsonProperty(PropertyName = "Language2Comment", DefaultValueHandling = DefaultValueHandling.Populate)]
    [PropertyIndex(6)]
    string Language2Comment { get; set; }

    [JsonProperty(PropertyName = "Drawer")]
    [PropertyIndex(7)]
    byte Drawer { get; set; }

    [JsonProperty(PropertyName = "Visible")]
    [DefaultValue(true)]
    [PropertyIndex(8)]
    bool Visible { get; set; }

    [JsonProperty(PropertyName = "State")]
    [PropertyIndex(9)]
    int State { get; set; }
  }
}