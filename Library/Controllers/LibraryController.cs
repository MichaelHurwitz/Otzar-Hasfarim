using Library.Models;
using Library.Service;
using Microsoft.AspNetCore.Mvc;
using Library.ViewModel;
using System;
using System.Threading.Tasks;

namespace Library.Controllers
{
    public class LibraryController : Controller
    {
        private readonly ILibraryService _libraryService;
        private readonly IShelfService _shelfService;

        public LibraryController(ILibraryService libraryService, IShelfService shelfService)
        {
            _libraryService = libraryService;
            _shelfService = shelfService;
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                var libraries = await _libraryService.GetAllLibraries();
                return View(libraries);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }
        }

        public IActionResult AddLibrary()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddLibrary(LibraryVm model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            try
            {
                var library = new LibraryModel
                {
                    Genre = model.Genre
                };
                await _libraryService.AddLibrary(library);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }
        }

        public async Task<IActionResult> EditLibrary(long id)
        {
            try
            {
                var library = await _libraryService.GetLibraryById(id);
                if (library == null)
                {
                    return NotFound();
                }

                var model = new LibraryVm
                {
                    Id = library.Id,
                    Genre = library.Genre
                };
                return View(model);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditLibrary(LibraryVm model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            try
            {
                var library = new LibraryModel
                {
                    Id = model.Id,
                    Genre = model.Genre
                };
                await _libraryService.UpdateLibrary(library);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }
        }

        public async Task<IActionResult> DeleteLibrary(long id)
        {
            try
            {
                var library = await _libraryService.GetLibraryById(id);
                if (library == null)
                {
                    return NotFound();
                }

                return View(library);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPost, ActionName("DeleteLibrary")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            try
            {
                await _libraryService.DeleteLibrary(id);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }
        }
    }
}
