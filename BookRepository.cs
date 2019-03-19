using System;
using System.Collections.Generic;
using System.Linq;

namespace BookList
{
    public class BookRepository : IBookRepository<Book>
    {
        private readonly List<Book> listBooks;
        private readonly IFileHandler<Book> fileHandler;

        public BookRepository(IFileHandler<Book> fileHandler)
        {
            this.fileHandler = fileHandler;
            listBooks = fileHandler.Load();
        }

        public Book Get(int id)
        {
            var index = listBooks.FindIndex(x => x.Id == id);
            return listBooks[index];
        }

        public void Add(Book obj)
        {
            listBooks.Add(obj);
        }

        public void Edit(int id, Book obj)
        {
            var index = listBooks.FindIndex(x => x.Id == id);
            listBooks[index] = obj;
        }

        public void Delete(int id)
        {
            var index = listBooks.FindIndex(x => x.Id == id);
            listBooks.RemoveAt(index);
        }

        public void SaveChanges()
        {
            fileHandler.Save(listBooks);
        }

        public void Show()
        {
            foreach (var book in listBooks)
            {
                Console.WriteLine($"Id: {book.Id}" +
                                  $"\nTitle: {book.Title}" +
                                  $"\nAuthor: {book.Author}\n");
            }
        }
    }
}
