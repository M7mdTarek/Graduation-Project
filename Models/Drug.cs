using Microsoft.AspNetCore.Http.HttpResults;
using static System.Net.Mime.MediaTypeNames;
using System.Security.Principal;

namespace Test.Models
{
    public class Drug
    {
        public int Id { get; set; }

        public string Name_Ar { get; set; }

        public string Name_en { get; set; }

        public string scientific_name_ar { get; set;}

        public string scientific_name_en { get; set; }

        public string classification_ar { get; set; }

        public string classification_en { get; set; }

        public string category_ar { get; set; }

        public string category_en { get; set; }

        public string description_ar { get; set; }

        public string description_en { get; set; }

        public string image { get; set; }

    }
   
}
