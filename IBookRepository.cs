namespace BookList
{
    public interface IBookRepository<T>
    {
        T Get(int id);
        void Add(T obj);
        void Edit(int id,T obj);
        void Delete(int id);
        void SaveChanges();
        void Show();
    }
}
