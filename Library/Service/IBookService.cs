using Library.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Library.Service
{
    public interface IBookService
    {
        Task<IEnumerable<BookModel>> GetAllBooks();
        Task<BookModel?> GetBookById(long id);
        Task AddBook(BookModel book);
        Task UpdateBook(BookModel book);
        Task DeleteBook(long id);
    }
}
