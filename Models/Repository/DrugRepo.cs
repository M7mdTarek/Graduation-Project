using MailKit.Search;
using Microsoft.EntityFrameworkCore;

namespace Test.Models.Repository
{
    public class DrugRepo : IAppRepo<Drug>
    {
        private readonly AppDbContext dbContext;

        public DrugRepo(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public List<Drug> GetAll() => dbContext.Set<Drug>().ToList();

        public Drug GetOne(object searchTerm)
        {
            var name = searchTerm.ToString();

            //var query = dbContext.Set<Drug>().FirstOrDefault(d => d.Name_Ar.Contains(name) || d.Name_en.Contains(name) ||
            //   d.scientific_name_ar.Contains(name) || d.scientific_name_en.Contains(name) ||
            //   d.classification_ar.Contains(name) || d.classification_en.Contains(name));
            //return query;


            var drugs = dbContext.Set<Drug>().ToList();
            Dictionary<Drug, int> similarityScores = new Dictionary<Drug, int>();

            foreach (var drug in drugs)
            {
                int similarity = CalculateLevenshteinDistance(name.ToLower(), drug.Name_Ar.ToLower());
                similarityScores.Add(drug, similarity);
            }

            // Find the drug with the lowest similarity score (most similar)
            var mostSimilarDrug = similarityScores.OrderBy(pair => pair.Value).FirstOrDefault().Key;

            return mostSimilarDrug;
        }

        private int CalculateLevenshteinDistance(string source, string target)
        {
            if (string.IsNullOrEmpty(source))
            {
                if (string.IsNullOrEmpty(target))
                    return 0;
                return target.Length;
            }

            if (string.IsNullOrEmpty(target))
                return source.Length;

            int sourceLength = source.Length;
            int targetLength = target.Length;

            var matrix = new int[sourceLength + 1, targetLength + 1];

            for (int i = 0; i <= sourceLength; matrix[i, 0] = i++) { }
            for (int j = 0; j <= targetLength; matrix[0, j] = j++) { }

            for (int i = 1; i <= sourceLength; i++)
            {
                for (int j = 1; j <= targetLength; j++)
                {
                    int cost = (target[j - 1] == source[i - 1]) ? 0 : 1;
                    matrix[i, j] = Math.Min(Math.Min(matrix[i - 1, j] + 1, matrix[i, j - 1] + 1), matrix[i - 1, j - 1] + cost);
                }
            }

            return matrix[sourceLength, targetLength];
        }
    }
}
