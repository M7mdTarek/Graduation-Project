using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Test.Authentication;
using Test.Data;
using Test.DTO;

namespace Test.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly AppDbContext dbContext;
        private readonly SignupHandler signupHandler;

        public AuthenticationController(AppDbContext dbContext, SignupHandler signupHandler)
        {
            this.dbContext = dbContext;
            this.signupHandler = signupHandler;
        }

        [HttpPost]
        [Route("signup")]
        public ActionResult SignUp(UserDto userdto)
        {
            // checking the email format
            if (!signupHandler.isValidEmail(userdto.email))
                return BadRequest("invalid email");

            // check the email is already used
            if (signupHandler.isOldEmail(userdto.email))
                return BadRequest("the email is already used");

            // checking the numeric inputs
            if (!signupHandler.isValidHeight(userdto.height))
                return BadRequest("invalid Height");

            if (!signupHandler.isValidWeight(userdto.weight))
                return BadRequest("invalid Weight");

            if (!signupHandler.isValidAge(userdto.age))
                return BadRequest("invalid Age");

            User user = new User()
            {
                Username = userdto.userName,
                Email = userdto.email,
                Age = userdto.age,
                Height = userdto.height,
                Password = userdto.password,
                Weight = userdto.weight,
                ismale = userdto.isMale
            };

            int userid = signupHandler.CreateUser(user);

            signupHandler.AddChronicsToUser(userid, userdto.selectedChronic);

            return Ok();
        }

    }
}
