using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Test.Models.Repository;

namespace Test.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class FetchDataController : ControllerBase
    {
        private readonly ChronicDiseaseRepo diseaseRepo;
        private readonly DrugRepo drugRepo;

        public FetchDataController(ChronicDiseaseRepo diseaseRepo, DrugRepo drugRepo)
        {
            this.diseaseRepo = diseaseRepo;
            this.drugRepo = drugRepo;
        }

        [HttpGet]
        [Route("getchronicdisease")]
        public ActionResult GetChronicDisease() => Ok(diseaseRepo.GetAll());

        [HttpPost]
        [Route("searchfordrug")]
        public ActionResult SearchForDrug(string searchTerm)
        {
            if (string.IsNullOrEmpty(searchTerm))
                return BadRequest("the string is empty");


            var drug = drugRepo.GetOne(searchTerm);

            if (drug == null)
                return NotFound("the drug not found");

            return Ok(drug);
        
        }
    }
}
