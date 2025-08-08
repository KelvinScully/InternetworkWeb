using DataAccessLayer.Objects.Inventory;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using MvcApp.Areas.Inventory.Models;
using MvcApp.Services;

namespace MvcApp.Areas.Inventory.Controllers
{
    [Area("Inventory")]
    public class LocationController(IInventoryService service) : Controller
    {
        private readonly IInventoryService _Service = service;

        // =======================
        // Page Routes (Views)
        // =======================

        [HttpGet("[area]/[controller]/{ShowInactive}")]
        public async Task<IActionResult> Index(bool ShowInactive)
        {
            var model = await _Service.GetItemLocation(ShowInactive);
            return View(model);
        }

        [HttpGet("[area]/[controller]/Create")]
        public async Task<IActionResult> Create()
        {
            ItemLocationModel model = new();
            return View(model);
        }

        [HttpGet("[area]/[controller]/Edit/{Id}")]
        public async Task<IActionResult> Edit(int Id)
        {
            ItemLocationModel model = await _Service.GetItemLocation(Id);
            return View(model);
        }

        // =======================
        // Form Actions
        // =======================

        [HttpPost("[area]/[controller]/Insert")]
        public async Task<IActionResult> Insert(ItemLocationModel model)
        {
            if (!ModelState.IsValid)
                return View("Create", model);

            // --- Pre-check for duplicate location name (case + trim insensitive)
            var existing = await _Service.GetItemLocation(true); // include inactive to fully enforce uniqueness
            if (existing.Any(x =>
                string.Equals(x.ItemLocationName?.Trim(), model.ItemLocationName?.Trim(), StringComparison.OrdinalIgnoreCase)))
            {
                ModelState.AddModelError(nameof(model.ItemLocationName), "That location name already exists.");
                return View("Create", model);
            }

            var result = await _Service.InsertItemLocation(model);
            if (!result.IsSuccessful)
            {
                ModelState.AddModelError(string.Empty, string.IsNullOrWhiteSpace(result.Message)
                    ? "Could not create the location."
                    : result.Message);
                return View("Create", model);
            }

            return RedirectToAction("Index", new { ShowInactive = false });
        }

        [HttpPost("[area]/[controller]/Update")]
        public async Task<IActionResult> Update(ItemLocationModel model)
        {
            if (!ModelState.IsValid)
                return View("Edit", model);

            await _Service.UpdateItemLocation(model);
            return RedirectToAction("Index", new { ShowInactive = false });
        }

        [HttpPost("[area]/[controller]/Delete/{Id}")]
        public async Task<IActionResult> Delete(int Id)
        {
            await _Service.DeleteItemLocation(Id);
            return RedirectToAction("Index", new { ShowInactive = false });
        }
    }
}
