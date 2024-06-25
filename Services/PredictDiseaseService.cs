using System.Net.Http;
using System.Text;
using System.Text.Json;
using Test.DTO;

namespace Test.Services
{
    public class PredictDiseaseService
    {
        private readonly HttpClient httpClient;

        public PredictDiseaseService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<List<DiseaseDto>> Predict(List<object> symptoms)
        {
            var url = "https://mahmoudadel.us-east-1.modelbit.com/v1/predict_Disease/latest";

            // to make the list as object so can send it as json
            var data = new { data = symptoms };

            var jsonContent = JsonSerializer.Serialize(data);
            var httpContent = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            var response = await httpClient.PostAsync(url, httpContent);
            try
            {
                response.EnsureSuccessStatusCode();

            }
            catch (Exception ex)
            {
                return null;
            }

            // to make the response as json
            var content = await response.Content.ReadAsStringAsync();

            //to map the json into response which is list of dto
            var diseaseResponse = JsonSerializer.Deserialize<DiseaseResponse>(content);

            return diseaseResponse.Diseases;

        }
    }
}
