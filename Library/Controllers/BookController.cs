using Library.Models;
using Library.Service;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Collections.Generic;
using Library.ViewModel;
using System.Linq;

namespace Library.Controllers
{
    public class BookController : Controller
    {
        private readonly IBookService _bookService;
        private readonly ISetService _setService;
        private readonly IShelfService _shelfService;

        public BookController(IBookService bookService,
            ISetService setService,
            IShelfService shelfService)
        {
            _bookService = bookService;
            _setService = setService;
            _shelfService = shelfService;
        }

        public async Task<IActionResult> Index(long setId)
        {
            try
            {
                var books = await _bookService
                    .GetAllBooks();
                var filteredBooks = books
                    .Where(b => b.SetId == setId)
                    .ToList();
                ViewBag.SetId = setId;
                return View(filteredBooks);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "internal server error");
            }
        }

        public IActionResult AddBook(long setId)
        {
            ViewBag.SetId = setId;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddBook(BookVm model)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.SetId = model.SetId;
                return View(model);
            }

            try
            {
                var book = new BookModel
                {
                    Title = model.Title,
                    Genre = model.Genre,
                    Height = model.Height,
                    Width = model.Width,
                    SetId = model.SetId
                };
                await _bookService
                    .AddBook(book);
                return RedirectToAction(nameof(Index), new { setId = model.SetId });
            }
            catch (ValidationException ex)
            {
                ModelState
                    .AddModelError("Warning", ex.Message);
                ViewBag.SetId = model.SetId;
                return View(model);
            }
            catch (Exception ex)
            {
                ModelState
                    .AddModelError("", ex.Message);
                ViewBag.SetId = model.SetId;
                return View(model);
            }
        }

        public async Task<IActionResult> EditBook(long id)
        {
            try
            {
                var book = await _bookService
                    .GetBookById(id);
                if (book == null)
                {
                    return NotFound();
                }

                var model = new BookVm
                {
                    Id = book.Id,
                    Title = book.Title,
                    Genre = book.Genre,
                    Height = book.Height,
                    Width = book.Width,
                    SetId = book.SetId
                };
                ViewBag.SetId = book.SetId;
                return View(model);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "internal server error");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditBook(BookVm model)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.SetId = model.SetId;
                return View(model);
            }

            try
            {
                var book = new BookModel
                {
                    Id = model.Id,
                    Title = model.Title,
                    Genre = model.Genre,
                    Height = model.Height,
                    Width = model.Width,
                    SetId = model.SetId
                };
                await _bookService
                    .UpdateBook(book);
                return RedirectToAction(nameof(Index), new { setId = model.SetId });
            }
            catch (ValidationException ex)
            {
                ModelState
                    .AddModelError("Warning", ex.Message);
                ViewBag.SetId = model.SetId;
                return View(model);
            }
            catch (Exception ex)
            {
                ModelState
                    .AddModelError("", ex.Message);
                ViewBag.SetId = model.SetId;
                return View(model);
            }
        }

        public async Task<IActionResult> DeleteBook(long id)
        {
            try
            {
                var book = await _bookService
                    .GetBookById(id);
                if (book == null)
                {
                    return NotFound();
                }

                return View(book);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "internal server error");
            }
        }

        [HttpPost, ActionName("DeleteBook")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            try
            {
                var book = await _bookService
                    .GetBookById(id);
                if (book == null)
                {
                    return NotFound();
                }

                var setId = book.SetId;
                await _bookService
                    .DeleteBook(id);

                return RedirectToAction(nameof(Index), new { setId = setId });
            }
            catch (Exception ex)
            {
                return StatusCode(500, "internal server error");
            }
        }
    }
}
