namespace Test.DTO
{
    public class SignupDto
    {
        public string userName { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public double height { get; set; }
        public double weight { get; set; }
        public int age { get; set; }
        public bool isMale { get; set; }
        public List<int> selectedChronic { get; set; }

}
}
