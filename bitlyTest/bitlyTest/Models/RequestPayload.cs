using Newtonsoft.Json;

namespace bitlyTest.Models
{
    public class RequestPayload
    {
        [JsonProperty("url", Required = Required.Always)]
        public string Url { get; set; }

        [JsonProperty("userId")]
        public string UserId { get; set; }
    }
}
