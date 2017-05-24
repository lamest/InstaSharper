using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace InstaSharper.Classes.Models
{
    public class InstaTag
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("media_count")]
        public int MediaCount { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }
    }
}
