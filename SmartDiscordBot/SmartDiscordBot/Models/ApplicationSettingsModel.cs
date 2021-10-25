using Newtonsoft.Json;

namespace SmartDiscordBot.Models
{
    public class ApplicationSettingsModel
    {
        [JsonProperty(PropertyName = "isManual")]
        public bool IsManual { get; set; }
    }
}
