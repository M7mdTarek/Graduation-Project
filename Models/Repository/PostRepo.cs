
namespace Test.Models.Repository
{
    public class PostRepo : IAppRepo<Post>
    {
        private readonly AppDbContext dbContext;

        public PostRepo(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public List<Post> GetAll() => dbContext.Set<Post>().ToList();

        public Post GetOne(object obj)
        {
            int id = (int)obj;
            return dbContext.Set<Post>().FirstOrDefault(p => p.id == id);
        }
    }
}
