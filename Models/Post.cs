using System.ComponentModel.DataAnnotations.Schema;

namespace Test.Models
{
    public class Post
    {
        public int id {  get; set; }

        public int disease_id { get; set; }

        public string title_ar { get; set; }

        public string title_en { get; set; }

        public string description_ar { get; set; }

        public string description_en { get; set; }

        public string image { get; set; }

        [NotMapped]
        public bool isAdvice {  get; set; } = false;
    }
}
