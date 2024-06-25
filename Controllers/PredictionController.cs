using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using Test.Models.Repository;
using Test.Services;

namespace Test.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class PredictionController : ControllerBase
    {
        private readonly SymptomRepo symptomRepo;
        private readonly PredictDiseaseService diseaseService;

        public PredictionController(SymptomRepo symptomRepo, PredictDiseaseService diseaseService)
        {
            this.symptomRepo = symptomRepo;
            this.diseaseService = diseaseService;
        }

        [HttpPost]
        [Route("predictdisease")]
        public async Task<ActionResult> PredictDisease(List<int> symptomsIds)
        {
            //get the english name of symptoms by their Ids
            var symptoms = symptomRepo.GetAll().Where(s => symptomsIds.Contains(s.id)).Select(s => s.Name_en).ToList<object>();
            
            //complete the list by 0 to be 17 size which the model accept
            while(symptoms.Count < 17)
            {
                symptoms.Add(0);
            }

            var result = await diseaseService.Predict(symptoms);

            if(result == null)
            {
                return NotFound("the modelbit request failed");
            }
            //to return the final result's key as dto property name
            var mappedResult = result.Select(d => new
            {
                enDiseaseName = d.enDiseaseName,
                arDiseaseName = d.arDiseaseName,
                confidence = d.confidence,
                enDiseaseDescription = d.enDiseaseDescription,
                arDiseaseDescription = d.arDiseaseDescription,
                enAdvices = d.enAdvices,
                arAdvices = d.arAdvices
            }).ToList();

            return Ok(mappedResult);
        }
    }
}
