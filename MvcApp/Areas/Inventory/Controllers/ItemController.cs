using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MvcApp.Areas.Inventory.Models;
using MvcApp.Services;

namespace MvcApp.Areas.Inventory.Controllers
{
    [Area("Inventory")]
    public class ItemController(IInventoryService service) : Controller
    {
        private readonly IInventoryService _Service = service;

        [HttpGet("[area]/[controller]/{showInactive:bool?}")]
        public async Task<IActionResult> Index(bool showInactive = false)
        {
            var model = await _Service.GetItem(showInactive);
            ViewData["ShowInactive"] = showInactive;
            return View(model);
        }

        [HttpGet("[area]/[controller]/Create")]
        public async Task<IActionResult> Create()
        {
            await LoadDropdownsAsync();
            return View(new ItemModel());
        }

        [HttpGet("[area]/[controller]/Edit/{id}")]
        public async Task<IActionResult> Edit(int id)
        {
            await LoadDropdownsAsync();
            var model = await _Service.GetItem(id);
            return View(model);
        }

        [HttpPost("[area]/[controller]/Insert")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Insert(ItemModel model)
        {
            if (!ModelState.IsValid)
            {
                await LoadDropdownsAsync();
                return View("Create", model);
            }

            // Pre-check for duplicate item name (case + trim insensitive)
            var existing = await _Service.GetItem(true); // include inactive in the check
            if (existing.Any(x =>
                string.Equals(x.ItemName?.Trim(), model.ItemName?.Trim(), StringComparison.OrdinalIgnoreCase)))
            {
                ModelState.AddModelError(nameof(model.ItemName), "That item name already exists.");
                await LoadDropdownsAsync();
                return View("Create", model);
            }

            var result = await _Service.InsertItem(model);
            if (!result.IsSuccessful)
            {
                ModelState.AddModelError("", result.Message);
                await LoadDropdownsAsync();
                return View("Create", model);
            }

            return RedirectToAction(nameof(Index), new { showInactive = false });
        }

        [HttpPost("[area]/[controller]/Update")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(ItemModel model)
        {
            if (!ModelState.IsValid)
            {
                await LoadDropdownsAsync();
                return View("Edit", model);
            }

            var result = await _Service.UpdateItem(model);
            if (!result.IsSuccessful)
            {
                ModelState.AddModelError("", result.Message);
                await LoadDropdownsAsync();
                return View("Edit", model);
            }

            return RedirectToAction(nameof(Index), new { showInactive = false });
        }

        [HttpPost("[area]/[controller]/Delete/{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _Service.DeleteItem(id);
            if (!result.IsSuccessful)
                TempData["Error"] = result.Message;

            return RedirectToAction(nameof(Index), new { showInactive = false });
        }

        private async Task LoadDropdownsAsync()
        {
            var categories = await _Service.GetItemCategory(false);
            ViewData["Categories"] = new SelectList(categories, "ItemCategoryId", "ItemCategoryName");

            var locations = await _Service.GetItemLocation(false);
            ViewData["Locations"] = new SelectList(locations, "ItemLocationId", "ItemLocationName");

            var statuses = await _Service.GetItemStatus(false);
            ViewData["Statuses"] = new SelectList(statuses, "ItemStatusId", "ItemStatusName");
        }
    }
}
