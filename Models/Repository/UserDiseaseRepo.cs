namespace Test.Models.Repository
{
    public class UserDiseaseRepo:IAppRepo<UserChronicDisease>
    {
        private readonly AppDbContext dbContext;

        public UserDiseaseRepo(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public List<UserChronicDisease> GetAll() => dbContext.Set<UserChronicDisease>().ToList();

        public UserChronicDisease GetOne(object id)
        {
            return dbContext.Set<UserChronicDisease>().SingleOrDefault(x => x.disease_id == (int)id);
        }
    }
}
