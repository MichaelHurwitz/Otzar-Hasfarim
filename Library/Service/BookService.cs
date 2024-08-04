using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Library.Data;
using Library.Models;
using Microsoft.EntityFrameworkCore;

namespace Library.Service
{
    public class BookService : IBookService
    {
        private readonly ApplicationDbContext _context;
        private readonly IShelfService _shelfService;
        private readonly ISetService _setService;
        private readonly ILibraryService _libraryService;

        public BookService(ApplicationDbContext context, 
            IShelfService shelfService, 
            ISetService setService, 
            ILibraryService libraryService)
        {
            _context = context;
            _shelfService = shelfService;
            _setService = setService;
            _libraryService = libraryService;
        }

        public async Task<IEnumerable<BookModel>> GetAllBooks() =>
        await _context.Books
                .ToListAsync();
        

        public async Task<BookModel?> GetBookById(long id) =>
        await _context.Books
                .FindAsync(id);
        

        public async Task<IEnumerable<BookModel>> GetBooksBySetId(long setId) =>
        await _context.Books
            .Where(b => b.SetId == setId)
            .ToListAsync();
       

        public async Task AddBook(BookModel book)
        {
            var validationMessage = await ValidateBook(book);
            if (!string.IsNullOrEmpty(validationMessage))
            {
                throw new ValidationException(validationMessage);
            }

            _context.Books
                .Add(book);
            await _context
                .SaveChangesAsync();
        }

        public async Task UpdateBook(BookModel book)
        {
            var validationMessage = await ValidateBook(book);
            if (!string.IsNullOrEmpty(validationMessage))
            {
                throw new ValidationException(validationMessage);
            }

            _context
                .Entry(book)
                .State = EntityState.Modified;
            await _context
                .SaveChangesAsync();
        }

        public async Task DeleteBook(long id)
        {
            var book = await _context.Books
                .FindAsync(id);
            if (book != null)
            {
                _context.Books
                    .Remove(book);
                await _context
                    .SaveChangesAsync();
            }
        }

        private async Task<string?> ValidateBook(BookModel book)
        {
            var set = await _setService
                .GetSetById(book.SetId);
            var shelf = await _shelfService
                .GetShelfById(set.ShelfId);
            var library = await _libraryService
                .GetLibraryById(shelf.LibraryId);
            var booksInSet = await GetBooksBySetId(book.SetId);

            string message = null;

            switch (true)
            {
                case true when book.Genre != library.Genre:
                    message = "The book's genre does not match the library's genre.";
                    break;

                case true when book.Height > shelf.Height:
                    message = "The book's height is greater than the shelf's height.";
                    break;

                case true when shelf.Height - book.Height >= 10:
                    message = "The book's height is more than 10 cm shorter than the shelf's height. There is excess space.";
                    break;

                case true when booksInSet
                .Sum(b => b.Width) + book.Width > shelf.Width:
                    message = "The total width of the books exceeds the shelf's width.";
                    break;
            }

            return message;
        }
    }

    public class ValidationException : Exception
    {
        public ValidationException(string message) : base(message) { }
    }
}
