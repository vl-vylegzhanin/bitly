using Newtonsoft.Json;

namespace bitlyTest.Models
{
    public class RequestPayload
    {
        [JsonProperty("uri", Required = Required.Always)]
        public string Uri { get; set; }

        public string UserId { get; set; }
    }
}
