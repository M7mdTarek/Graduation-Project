namespace Test.Models.Repository
{
    public interface IAppRepo<T>
    {
        List<T> GetAll();

        T GetById(int id);
    }
}
