using System.Text.Json.Serialization;

namespace Test.DTO
{
    public class DiseaseDto
    {
        [JsonPropertyName("disease")]
        public string enDiseaseName { get; set; }

        [JsonPropertyName("disease_name_ar")]
        public string arDiseaseName { get; set; }

        [JsonPropertyName("confidence")]
        public double confidence { get; set; }

        [JsonPropertyName("description_en")]
        public string enDiseaseDescription { get; set; }

        [JsonPropertyName("description_ar")]
        public string arDiseaseDescription { get; set; }

        [JsonPropertyName("precautions_en")]
        public List<string> enAdvices { get; set; }

        [JsonPropertyName("precautions_ar")]
        public List<string> arAdvices { get; set; }
        
    }

}

