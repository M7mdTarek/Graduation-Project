using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.EntityFrameworkCore;
using MimeKit;
using Test.Helper;
using Test.Models;

namespace Test.Authentication
{
    public class ForgetPasswordHandler
    {
        private readonly AppDbContext dbContext;
        private readonly MailOptions mailOptions;
        private readonly SignupHandler signupHandler;

        public ForgetPasswordHandler(AppDbContext dbContext,MailOptions mailOptions,SignupHandler signupHandler)
        {
            this.dbContext = dbContext;
            this.mailOptions = mailOptions;
            this.signupHandler = signupHandler;
        }

        public string GenerateOTP()
        {
            Random random = new Random();

            string otp = random.Next(100000, 999999).ToString("D6");

            return otp;
        }
        public async Task SendMail(string emailTo, string otp)
        {
            var email = new MimeMessage
            {
                Sender = MailboxAddress.Parse(mailOptions.Email),
                Subject = mailOptions.Subject
            };

            email.To.Add(MailboxAddress.Parse(emailTo));

            var bodyBuilder = new BodyBuilder();

            bodyBuilder.HtmlBody = $"The Verification Code From MediSim To Reset Your Password is : {otp}";

            email.Body = bodyBuilder.ToMessageBody();

            email.From.Add(new MailboxAddress(mailOptions.DisplayName,mailOptions.Email));

            using var smtp = new SmtpClient();

            smtp.Connect(mailOptions.Host, mailOptions.Port,SecureSocketOptions.StartTls );
            smtp.Authenticate(mailOptions.Email, mailOptions.Password);

            await smtp.SendAsync(email);

            smtp.Disconnect(true);
        }
        public void StoreData(string email, string otp)
        {
            var row = new Email_OTP()
            {
                Email = email,
                OTP = otp
            };

            dbContext.Set<Email_OTP>().Add(row);

            dbContext.SaveChanges();
        }
        public void CheckPastOtp(string email)
        {
            var useremail = dbContext.Set<Email_OTP>().Find(email);
            if (useremail != null)
                dbContext.Set<Email_OTP>().Remove(useremail);
            dbContext.SaveChanges() ;
        }
        public async Task<bool> ValidateOTP(string email,string otp)
        {
            var user = dbContext.Set<Email_OTP>().Find(email);

            if (user.OTP != otp)
                return false;

            dbContext.Set<Email_OTP>().Remove(user);
            await dbContext.SaveChangesAsync() ;
            return true;
        }
        public async Task UpdatePassword(string email, string password)
        {
            string hashed = signupHandler.HashingPassword(password);
            var user = dbContext.Set<User>().FirstOrDefault(u => u.Email == email);
            user.Password = hashed;
            dbContext.Set<User>().Update(user);

            await dbContext.SaveChangesAsync();

        }
    }
}
