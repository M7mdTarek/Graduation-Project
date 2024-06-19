
namespace Test.Models.Repository
{
    public class ChronicDiseaseRepo : IAppRepo<Chronic_disease>
    {
        private readonly AppDbContext dbContext;

        public ChronicDiseaseRepo(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public List<Chronic_disease> GetAll() => dbContext.Set<Chronic_disease>().ToList();
        
        public Chronic_disease GetOne(object id) 
        {
            return dbContext.Set<Chronic_disease>().SingleOrDefault(x => x.Id == (int) id);
        } 
        
    }
}
