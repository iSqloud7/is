using LibraryApp.Web.Models.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryApp.Service.Interface
{
    public interface IBookService
    {
        Book Create(Book book);
        Book Update(Book book);
        Book Delete(Guid ID);
        Book? GetByID(Guid ID);
        List<Book> GetAll();
    }
}
