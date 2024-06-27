using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Test.Authentication;
using Test.Models.Repository;

namespace Test.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class FetchDataController : ControllerBase
    {
        private readonly ChronicDiseaseRepo diseaseRepo;
        private readonly DrugRepo drugRepo;
        private readonly SymptomRepo symptomRepo;
        private readonly ValidateTokenHandler tokenHandler;
        private readonly UserDiseaseRepo userDiseaseRepo;
        private readonly PostRepo postRepo;

        public FetchDataController(ChronicDiseaseRepo diseaseRepo, DrugRepo drugRepo, SymptomRepo symptomRepo,
            ValidateTokenHandler tokenHandler, UserDiseaseRepo userDiseaseRepo, PostRepo postRepo )
        {
            this.diseaseRepo = diseaseRepo;
            this.drugRepo = drugRepo;
            this.symptomRepo = symptomRepo;
            this.tokenHandler = tokenHandler;
            this.userDiseaseRepo = userDiseaseRepo;
            this.postRepo = postRepo;
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

        [HttpGet]
        [Route("getsymptoms")]
        public ActionResult GetSymptoms() => Ok(symptomRepo.GetAll());

        [HttpGet]
        [Route("getposts")]
        public ActionResult GetPosts()
        {
            //get the user id from the token
            string token = HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
            int userid = tokenHandler.GetUserId(token);

            //get the id of the user's chronic disease
            var chronicDiseases = userDiseaseRepo.GetAll().Where(d => d.user_id == userid).Select(d => d.disease_id).ToList();

            //get all posts and set isadvice true if the user has this post
            var posts = postRepo.GetAll()
                .Select(p =>
                {
                    // Check if the post's disease_id is in the list of chronicDiseases
                    p.isAdvice = chronicDiseases.Contains(p.disease_id);
                    return p;
                }).ToList();

            return Ok(posts);
        }
    }
}
