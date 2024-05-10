namespace Test.DTO
{
    public class UserDto
    {
        public string userName { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public decimal height { get; set; }
        public decimal weight { get; set; }
        public int age { get; set; }
        public bool isMale { get; set; }
        public List<int> selectedChronic { get; set; }

}
}
