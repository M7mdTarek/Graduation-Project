using System.Text.Json;
using System.Text;
using Test.DTO;
using Azure.Core.Serialization;


namespace Test.Services
{
    public class PredictSkinDiseaseService
    {
        private readonly HttpClient httpClient;

        public PredictSkinDiseaseService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<string> ConvertTo64(IFormFile image)
        {
            // Convert the image to bytes
            using var ms = new MemoryStream();
            await image.CopyToAsync(ms);
            byte[] imgBytes = ms.ToArray();

            // Encode the bytes to base64
            string imgBase64 = Convert.ToBase64String(imgBytes);
            return imgBase64;
        }

        public async Task<SkinDto> Predict(string imagebase64)
        {
            string url = "https://mahmoudadel.us-east-1.modelbit.com/v1/predict_image/latest";

            // to make the list as object so can send it as json
            var data = new {data = imagebase64 };

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
            //to map the json response into response of dto (ignoring data key in original response)
            var skinResponse =  JsonSerializer.Deserialize<SkinResponse>(content);

            return skinResponse.result;
            
        }
    }
}
