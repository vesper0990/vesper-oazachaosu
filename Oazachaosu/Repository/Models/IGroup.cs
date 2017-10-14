﻿using Newtonsoft.Json;
using Repository.Helpers;
using Repository.Models.Language;

namespace Repository.Models
{
    public interface IGroup
    {
        [JsonProperty(PropertyName = "Id")]
        [PropertyIndex(0)]
        long Id { get; set; }

        [JsonProperty(PropertyName = "UserId")]
        [JsonIgnore]
        [PropertyIndex(1)]
        long UserId { get; set; }

        [JsonProperty(PropertyName = "Name")]
        [PropertyIndex(2)]
        string Name { get; set; }

        [JsonProperty(PropertyName = "Language1", DefaultValueHandling = DefaultValueHandling.Populate)]
        [JsonConverter(typeof(LanguageJsonCoverter))]
        [PropertyIndex(3)]
        LanguageType Language1 { get; set; }

        [JsonProperty(PropertyName = "Language2", DefaultValueHandling = DefaultValueHandling.Populate)]
        [JsonConverter(typeof(LanguageJsonCoverter))]
        [PropertyIndex(4)]
        LanguageType Language2 { get; set; }

        [JsonProperty(PropertyName = "State")]
        [PropertyIndex(5)]
        int State { get; set; }
    }
}