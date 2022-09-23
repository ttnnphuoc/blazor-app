using Newtonsoft.Json;

namespace BlazorApp1.Model.Common
{
    public class ConnectionStringSettings
    {
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string Connection { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string DatabaseName { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string DatabaseUser { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string DatabasePassword { get; set; }
    }
}
