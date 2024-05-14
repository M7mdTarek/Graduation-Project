namespace Test.DTO
{
    public class MailDto
    {
        public string mail {  get; set; }
        public string? otp { get; set; } = null;
        public string? password { get; set; } = null;
        public string? confirmpassword { get; set; } = null;
    }
}
