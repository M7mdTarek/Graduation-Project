
namespace Test.Models.Repository
{
    public class SymptomRepo : IAppRepo<Symptom>
    {
        private readonly AppDbContext dbContext;

        public SymptomRepo(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public List<Symptom> GetAll() => dbContext.Set<Symptom>().ToList();

        public Symptom GetOne(object id)
        {
            int Id = (int)id;
            var symptoms = dbContext.Set<Symptom>().ToList();

            return symptoms.FirstOrDefault(s => Id == s.id);
        }
    }
}
