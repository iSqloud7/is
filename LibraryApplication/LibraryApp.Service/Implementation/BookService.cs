using LibraryApp.Repository.Interface;
using LibraryApp.Service.Interface;
using LibraryApp.Web.Models.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryApp.Service.Implementation
{
    public class BookService : IBookService
    {
        private readonly IRepository<Book>? BookRepository;

        public BookService(IRepository<Book>? bookRepository)
        {
            BookRepository = bookRepository;
        }

        public Book Create(Book book)
        {
            return this.BookRepository.Create(book);
        }

        public Book Update(Book book)
        {
            return this.BookRepository.Update(book);
        }

        public Book Delete(Guid ID)
        {
            var book = GetByID(ID);

            if(book == null)
            {
                throw new Exception("Book not found!");
            }

            return this.BookRepository.Delete(book);
        }

        public Book? GetByID(Guid ID)
        {
            return this.BookRepository.Get(selector: book => book, filter: book => book.Equals(ID));
        }

        public List<Book> GetAll()
        {
            return this.BookRepository.GetAll(selector: book => book).ToList();
        }
    }
}
