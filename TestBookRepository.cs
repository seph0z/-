using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BookList;
using Moq;

namespace TestBookList
{
    [TestClass]
    public class TestBookRepository
    {

        Book book = new Book{ Id = 25, Title = "Alchemist", Author = "Paulo Coelho" };

        [TestMethod]
        public void Ctor_GetParameters_ExpectedWork()
        {
            var mock = new Mock<IFileHandler<Book>>();

            var subject = new BookRepository(mock.Object);

            mock.Verify(x=>x.Load(), Times.Once);
        }

        [TestMethod]
        public void Get_BookExist_SholdReturn()
        {
            var mock = new Mock<IBookRepository<Book>>();
            mock.Setup(x => x.Get(It.IsAny<int>())).Returns(book);

            var subject = mock.Object;
            var result = subject.Get(25);

            Assert.IsNotNull(subject);
            Assert.AreEqual("Paulo Coelho", result.Author);
            mock.Verify(x => x.Get(It.IsAny<int>()), Times.Once);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Get_BookNotFound_ExpectedException()
        {
            var mock = new Mock<IFileHandler<Book>>();

            mock.Setup(x => x.Load()).Returns(new List<Book> {book});

            var subject = new BookRepository(mock.Object);
            var result = subject.Get(100);
        }

        [TestMethod]
        public void Add_AddBook_SholdReturn()
        {
            var mock = new Mock<IFileHandler<Book>>();
            mock.Setup(x => x.Load()).Returns(new List<Book>());

            var subject = new BookRepository(mock.Object);
            subject.Add(book);
            var result = subject.Get(25);
            
            Assert.IsNotNull(result);
            Assert.AreEqual(25, result.Id);
        }

        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public void Add_BookDoesNotNull_ExpecterdException()
        {
            var mock = new Mock<IFileHandler<Book>>();
            mock.Setup(x => x.Load()).Returns(new List<Book>());

            var subject = new BookRepository(mock.Object);
            subject.Add(null);

            var result = subject.Get(25);
        }

        [TestMethod]
        public void Edit_EditBook_ShouldBeChanged()
        {
            var mock = new Mock<IFileHandler<Book>>();
            mock.Setup(x => x.Load()).Returns(new List<Book> {book});

            var subject = new BookRepository(mock.Object);
            subject.Edit(25, new Book{Id = 100, Title = "new title", Author = "new author"});
            var result = subject.Get(100);

            Assert.IsNotNull(result);
            Assert.AreEqual("new title", result.Title);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Edit_BookNotFound_ExpectedException()
        {
            var mock = new Mock<IFileHandler<Book>>();
            mock.Setup(x => x.Load()).Returns(new List<Book> {book});

            var subject = new BookRepository(mock.Object);

            subject.Edit(104, new Book());
        }

        [TestMethod]
        public void Delete_DeleteBook_NoException()
        {
            var mock = new Mock<IFileHandler<Book>>();
            mock.Setup(x => x.Load()).Returns(new List<Book> {book});

            var subject = new BookRepository(mock.Object);

            subject.Delete(25);
        }

        [TestMethod]
        [ExpectedException(typeof(System.ArgumentOutOfRangeException))]
        public void Delete_BookNotFound_ExpectedException()
        {
            Mock<IFileHandler<Book>> mock = new Mock<IFileHandler<Book>>();

            mock.Setup(x => x.Load()).Returns(new List<Book> { });

            var subject = new BookRepository(mock.Object);
            subject.Delete(10);
        }

        [TestMethod]
        public void Show_CallShow_CallAtLeastOnce()
        {
            var mock = new Mock<IBookRepository<Book>>();
            mock.Setup(x => x.Show());

            var subject = mock.Object;
            subject.Show();           

            mock.Verify(x => x.Show(), Times.Once);
        }
    }
}
