using System.Text.Json.Serialization;

namespace Test.DTO
{
    public class SkinResponse
    {
        [JsonPropertyName("data")]
        public SkinDto result { get; set; }
    }
}
