using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace Web.Domain
{
    public class CommonResponse
    {
        [JsonPropertyName("code")]
        [JsonProperty("code", Order = int.MinValue)]
        public virtual int Code { get; set; } = 0;
        [JsonPropertyName("message")]
        [JsonProperty("message", Order = int.MinValue)]
        public virtual string? Message { get; set; } = string.Empty;
        [JsonPropertyName("data")]
        [JsonProperty("data", Order = int.MaxValue)]
        public virtual object? Data { get; set; }
    }
}
