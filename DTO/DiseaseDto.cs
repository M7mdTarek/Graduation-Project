namespace Test.DTO
{
    public class DiseaseDto
    {
        public string enDiseaseName { get; set; }

        public string arDiseaseName { get; set; }

        public double confidence { get; set; }

        public string enDiseaseDescription { get; set; }

        public string arDiseaseDescription { get; set; }

        public List<string> enAdvices { get; set; }

        public List<string> arAdvices { get; set; }


    }
}
