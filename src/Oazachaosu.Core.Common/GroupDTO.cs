﻿using Newtonsoft.Json;
using System;

namespace Oazachaosu.Core.Common
{
    public class GroupDTO
    {
        [JsonProperty("Id")]
        public long Id { get; set; }
        [JsonProperty("N")]
        public string Name { get; set; }
        [JsonProperty("L1")]
        public LanguageType Language1 { get; set; }
        [JsonProperty("L2")]
        public LanguageType Language2 { get; set; }
        [JsonProperty("S")]
        public int State { get; set; }
        [JsonProperty("CD")]
        public DateTime CreationDate { get; set; }
    }
}