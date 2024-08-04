using Library.Models;
using Library.Service;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Collections.Generic;
using Library.ViewModel;
using System.Linq;
using System;

namespace Library.Controllers
{
    public class ShelfController : Controller
    {
        private readonly IShelfService _shelfService;
        private readonly ILibraryService _libraryService;

        public ShelfController(IShelfService shelfService, ILibraryService libraryService)
        {
            _shelfService = shelfService;
            _libraryService = libraryService;
        }

        public async Task<IActionResult> Index(long libraryId)
        {
            try
            {
                var shelves = await _shelfService.GetAllShelves();
                var filteredShelves = shelves.Where(s => s.LibraryId == libraryId).ToList();
                ViewBag.LibraryId = libraryId;
                return View(filteredShelves);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }
        }

        public async Task<IActionResult> DetailsShelf(long id)
        {
            var shelf = await _shelfService.GetShelfById(id);
            if (shelf == null)
            {
                return NotFound();
            }
            return View(shelf);
        }

        public IActionResult AddShelf(long libraryId)
        {
            ViewBag.LibraryId = libraryId;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddShelf(ShelfVm model)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.LibraryId = model.LibraryId;
                return View(model);
            }

            try
            {
                var shelf = new ShelfModel
                {
                    Height = model.Height,
                    Width = model.Width,
                    LibraryId = model.LibraryId
                };
                await _shelfService.AddShelf(shelf);
                return RedirectToAction(nameof(Index), new { libraryId = model.LibraryId });
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }
        }

        public async Task<IActionResult> EditShelf(long id)
        {
            try
            {
                var shelf = await _shelfService.GetShelfById(id);
                if (shelf == null)
                {
                    return NotFound();
                }

                var model = new ShelfVm
                {
                    Id = shelf.Id,
                    Height = shelf.Height,
                    Width = shelf.Width,
                    LibraryId = shelf.LibraryId
                };
                ViewBag.LibraryId = shelf.LibraryId;
                return View(model);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditShelf(ShelfVm model)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.LibraryId = model.LibraryId;
                return View(model);
            }

            try
            {
                var shelf = new ShelfModel
                {
                    Id = model.Id,
                    Height = model.Height,
                    Width = model.Width,
                    LibraryId = model.LibraryId
                };
                await _shelfService.UpdateShelf(shelf);
                return RedirectToAction(nameof(Index), new { libraryId = model.LibraryId });
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }
        }

        public async Task<IActionResult> DeleteShelf(long id)
        {
            try
            {
                var shelf = await _shelfService.GetShelfById(id);
                if (shelf == null)
                {
                    return NotFound();
                }

                return View(shelf);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPost, ActionName("DeleteShelf")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            try
            {
                var shelf = await _shelfService.GetShelfById(id);
                if (shelf == null)
                {
                    return NotFound();
                }

                var libraryId = shelf.LibraryId;
                await _shelfService.DeleteShelf(id);

                return RedirectToAction("Index", new { libraryId = libraryId });
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }
        }
    }
}
