using System.Text.Json.Serialization;

namespace Test.DTO
{
    public class SkinDto
    {
        [JsonPropertyName("en_disease_name")]
        public string enDiseaseName { get; set; }

        [JsonPropertyName("ar_disease_name")]
        public string arDiseaseName { get; set; }

        [JsonPropertyName("en_description")]
        public string enDiseaseDescription { get; set; }

        [JsonPropertyName("ar_description")]
        public string arDiseaseDescription { get; set; }

    }
}
