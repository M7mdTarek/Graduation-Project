using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Test.Authentication;
using Test.Models;
using Test.DTO;
using Azure.Core;

namespace Test.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly LoginHandler loginHandler;
        private readonly SignupHandler signupHandler;
        private readonly ForgetPasswordHandler forgetPasswordHandler;

        public AuthenticationController(LoginHandler loginHandler, SignupHandler signupHandler,ForgetPasswordHandler forgetPasswordHandler)
        {
            this.loginHandler = loginHandler;
            this.signupHandler = signupHandler;
            this.forgetPasswordHandler = forgetPasswordHandler;
        }

        [HttpPost]
        [Route("signup")]
        public ActionResult SignUp(SignupDto request)
        {
            // checking the email format
            if (!signupHandler.isValidEmail(request.email))
                return BadRequest("invalid email");

            // check the email is already used
            if (signupHandler.isExistEmail(request.email))
                return BadRequest("the email is already used");

            // checking the numeric inputs
            if (!signupHandler.isValidHeight(request.height))
                return BadRequest("invalid Height");

            if (!signupHandler.isValidWeight(request.weight))
                return BadRequest("invalid Weight");

            if (!signupHandler.isValidAge(request.age))
                return BadRequest("invalid Age");

            
            User user = new User()
            {
                Username = request.userName,
                Email = request.email,
                Age = request.age,
                Height = request.height,
                Password = signupHandler.HashingPassword(request.password),
                Weight = request.weight,
                ismale = request.isMale
            };

            int userid = signupHandler.CreateUser(user);

            signupHandler.AddChronicsToUser(userid, request.selectedChronic);

            var token = loginHandler.CreateToken(user);
            return Ok(new {token});
        }

        [HttpPost]
        [Route("login")]
        public ActionResult Login(LoginDto request)
        {
            var checker = loginHandler.CheckeMail(request.email, request.password);
            if (checker.token == "email")
                return BadRequest("the email is wrong");

            if (checker.token == "password")
                return BadRequest("the password is wrong");

            return Ok(new { checker.token, userName = checker.user?.Username});
        }

        [HttpPost]
        [Route("forgetpassword")]
        public async Task<ActionResult> ForgetPassword(MailDto request)
        {
            // check the email is exist
            //if (!signupHandler.isExistEmail(email))
            //    return BadRequest("the email is not found");

            // check if the otp has been sent before
            forgetPasswordHandler.CheckPastOtp(request.mail);

            string otp = forgetPasswordHandler.GenerateOTP();

            // send mail with otp here
            await forgetPasswordHandler.SendMail(request.mail, otp);

            forgetPasswordHandler.StoreData(request.mail, otp);

            return Ok("the mail has ben sent successfully");
        }

        [HttpPost]
        [Route("verifyotp")]
        public async Task<ActionResult> VerifyOtp(MailDto request)
        {
            var isvalid = await forgetPasswordHandler.ValidateOTP(request.mail,request.otp);
            if (isvalid)
                return Ok("the OTP is valid ");
            else return BadRequest("the OTP is invalid ");
        }

        [HttpPost]
        [Route("changepassword")]
        public async Task<ActionResult> ChangePassword(MailDto request)
        {
            if (request.password != request.confirmpassword)
                return BadRequest("the two passwords don't match");

            await forgetPasswordHandler.UpdatePassword(request.mail, request.password);

            return Ok("the password updated successfully");
        }
    }
}
