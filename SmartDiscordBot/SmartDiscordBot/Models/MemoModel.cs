using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmartDiscordBot.Models
{
    public class MemoModel
    {
        [JsonProperty(PropertyName = "title")]
        public string Title { get; set; }

        [JsonProperty(PropertyName = "url")]
        public string ImageUrl { get; set; }

        [JsonProperty(PropertyName = "permalink")]
        public string PostUrl { get; set; }
    }
}
