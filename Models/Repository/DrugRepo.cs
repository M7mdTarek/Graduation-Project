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
            // Query for drugs
            var drugs = dbContext.Set<Drug>().ToList();
            Dictionary<Drug, double> similarityScores = new Dictionary<Drug, double>();

            foreach (var drug in drugs)
            {
                double arabicScore = GetSimilarityScore(name.ToLower(), drug.Name_Ar.ToLower());
                double englishScore = GetSimilarityScore(name.ToLower(), drug.Name_en.ToLower());

                // Combine the scores, giving equal weight to Arabic and English
                double combinedScore = (arabicScore + englishScore) / 2;

                similarityScores.Add(drug, combinedScore);
            }

            // Find the drug with the highest similarity score (most similar)
            var mostSimilarDrug = similarityScores.OrderByDescending(pair => pair.Value).FirstOrDefault().Key;

            return mostSimilarDrug;
        }
        // algorithms to search better for the drug
        private double GetSimilarityScore(string source, string target)
        {
            double levenshteinScore = 1.0 / (1.0 + CalculateLevenshteinDistance(source, target));
            double jaroWinklerScore = CalculateJaroWinklerDistance(source, target);
            double tokenScore = CalculateTokenSimilarity(source, target);

            // Weighted sum of different similarity scores
            return 0.5 * levenshteinScore + 0.3 * jaroWinklerScore + 0.2 * tokenScore;
        }

        private int CalculateLevenshteinDistance(string source, string target)
        {
            if (string.IsNullOrEmpty(source))
                return string.IsNullOrEmpty(target) ? 0 : target.Length;
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
                    if (char.ToLower(target[j - 1]) == char.ToLower(source[i - 1]))
                        cost = 0;
                    else if (char.IsLetter(target[j - 1]) && char.IsLetter(source[i - 1]))
                        cost = 2;
                    else
                        cost = 1;

                    matrix[i, j] = Math.Min(Math.Min(matrix[i - 1, j] + 1, matrix[i, j - 1] + 1), matrix[i - 1, j - 1] + cost);
                }
            }

            return matrix[sourceLength, targetLength];
        }

        private double CalculateJaroWinklerDistance(string source, string target)
        {
            if (source == target)
                return 1.0;

            int s_len = source.Length;
            int t_len = target.Length;

            if (s_len == 0 || t_len == 0)
                return 0.0;

            int match_distance = Math.Max(s_len, t_len) / 2 - 1;

            bool[] s_matches = new bool[s_len];
            bool[] t_matches = new bool[t_len];

            int matches = 0;
            int transpositions = 0;

            for (int i = 0; i < s_len; i++)
            {
                int start = Math.Max(0, i - match_distance);
                int end = Math.Min(i + match_distance + 1, t_len);

                for (int s = start; s < end; s++)
                {
                    if (t_matches[s])
                        continue;
                    if (source[i] != target[s])
                        continue;
                    s_matches[i] = true;
                    t_matches[s] = true;
                    matches++;
                    break;
                }
            }

            if (matches == 0)
                return 0.0;

            int k = 0;
            for (int i = 0; i < s_len; i++)
            {
                if (!s_matches[i])
                    continue;
                while (!t_matches[k])
                    k++;
                if (source[i] != target[k])
                    transpositions++;
                k++;
            }

            double m = matches;
            double j = ((m / s_len) + (m / t_len) + ((m - transpositions / 2.0) / m)) / 3.0;
            double p = 0.1;
            int l = 0;

            for (int i = 0; i < Math.Min(4, Math.Min(source.Length, target.Length)); i++)
            {
                if (source[i] == target[i])
                    l++;
                else
                    break;
            }

            return j + l * p * (1 - j);
        }

        private double CalculateTokenSimilarity(string source, string target)
        {
            var sourceTokens = source.Split(' ');
            var targetTokens = target.Split(' ');

            double totalScore = 0.0;
            foreach (var sourceToken in sourceTokens)
            {
                double maxScore = 0.0;
                foreach (var targetToken in targetTokens)
                {
                    double score = CalculateJaroWinklerDistance(sourceToken, targetToken);
                    if (score > maxScore)
                        maxScore = score;
                }
                totalScore += maxScore;
            }

            return totalScore / sourceTokens.Length;
        }
    }
}

