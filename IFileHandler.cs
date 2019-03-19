using System.Collections.Generic;

namespace BookList
{
    public interface IFileHandler<T>
    {
        List<T> Load();
        void Save(List<T> obj);
    }
}
