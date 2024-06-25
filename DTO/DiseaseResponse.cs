using System.Text.Json.Serialization;

namespace Test.DTO
{
    public class DiseaseResponse
    {
        [JsonPropertyName("data")]
        public List<DiseaseDto> Diseases { get; set; }
    }
}
