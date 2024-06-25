using System.Net.Http;
using System.Text;
using System.Text.Json;

namespace Test.Services
{
    public class PredictDiseaseService
    {
        private readonly HttpClient httpClient;

        public PredictDiseaseService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<string> Predict(List<object> data)
        {
            var url = "https://mahmoudadel.us-east-1.modelbit.com/v1/predict_Disease/latest";
            var jsonContent = JsonSerializer.Serialize(data);
            var httpContent = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            var response = await httpClient.PostAsync(url, httpContent);
            //response.EnsureSuccessStatusCode();

            return await response.Content.ReadAsStringAsync();
        }
    }
}
