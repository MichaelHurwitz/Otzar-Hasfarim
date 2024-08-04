using Library.Models;
using Library.Service;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Collections.Generic;
using Library.ViewModel;
using System.Linq;

namespace Library.Controllers
{
    public class SetController : Controller
    {
        private readonly ISetService _setService;
        private readonly IShelfService _shelfService;

        public SetController(ISetService setService, IShelfService shelfService)
        {
            _setService = setService;
            _shelfService = shelfService;
        }

        public async Task<IActionResult> Index(long shelfId)
        {
            try
            {
                var sets = await _setService
                    .GetAllSets();
                var filteredSets = sets
                    .Where(s => s.ShelfId == shelfId)
                    .ToList();
                ViewBag.ShelfId = shelfId;
                return View(filteredSets);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }
        }

        public async Task<IActionResult> DetailsSet(long id)
        {
            var set = await _setService
                .GetSetById(id);
            if (set == null)
            {
                return NotFound();
            }
            return View(set);
        }

        public IActionResult AddSet(long shelfId)
        {
            ViewBag.ShelfId = shelfId;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddSet(SetVm model)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.ShelfId = model.ShelfId;
                return View(model);
            }

            try
            {
                var set = new SetModel
                {
                    SetName = model.SetName,
                    ShelfId = model.ShelfId
                };
                await _setService
                    .AddSet(set);
                return RedirectToAction(nameof(Index), new { shelfId = model.ShelfId });
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }
        }

        public async Task<IActionResult> EditSet(long id)
        {
            try
            {
                var set = await _setService
                    .GetSetById(id);
                if (set == null)
                {
                    return NotFound();
                }

                var model = new SetVm
                {
                    Id = set.Id,
                    SetName = set.SetName,
                    ShelfId = set.ShelfId
                };
                ViewBag.ShelfId = set.ShelfId;
                return View(model);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditSet(SetVm model)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.ShelfId = model.ShelfId;
                return View(model);
            }

            try
            {
                var set = new SetModel
                {
                    Id = model.Id,
                    SetName = model.SetName,
                    ShelfId = model.ShelfId
                };
                await _setService
                    .UpdateSet(set);
                return RedirectToAction(nameof(Index), new { shelfId = model.ShelfId });
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }
        }

        public async Task<IActionResult> DeleteSet(long id)
        {
            try
            {
                var set = await _setService
                    .GetSetById(id);
                var shelf = await _shelfService
                    .GetShelfById(id);
                if (set == null)
                {
                    return NotFound();
                }

                return View(set);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPost, ActionName("DeleteSet")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            try
            {
                var set = await _setService.GetSetById(id);
                if (set == null)
                {
                    return NotFound();
                }

                var shelfId = set.ShelfId;
                await _setService.DeleteSet(id);

                return RedirectToAction("Index", new { shelfId = shelfId });
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }
        }

    }
}
