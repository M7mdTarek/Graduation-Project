using System.ComponentModel.DataAnnotations;

namespace Test.Models
{
    public class Email_OTP
    {
        public string Email {  get; set; }

        [Length(6,6)]
        public string OTP { get; set; }
    }
}
