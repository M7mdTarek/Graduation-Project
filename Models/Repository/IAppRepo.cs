namespace Test.Models.Repository
{
    public interface IAppRepo<T>
    {
        List<T> GetAll();

        // object for find by id or name
        T GetOne(object obj);
    }
}
