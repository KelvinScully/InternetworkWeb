using DataAccessLayer.Objects.Inventory;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using MvcApp.Areas.Inventory.Models;
using MvcApp.Services;

namespace MvcApp.Areas.Inventory.Controllers
{
    [Area("Inventory")]
    public class CategoryController(IInventoryService service) : Controller
    {
        private readonly IInventoryService _Service = service;

        // =======================
        // Page Routes (Views)
        // =======================

        [HttpGet("[area]/[controller]/{ShowInactive}")]
        public async Task<IActionResult> Index(bool ShowInactive)
        {
            if (User.IsInRole("Guest"))
                return RedirectToAction("Gate", "Entry");

            var model = await _Service.GetItemCategory(ShowInactive);
            return View(model);
        }

        [HttpGet("[area]/[controller]/Create")]
        public async Task<IActionResult> Create()
        {
            if (User.IsInRole("Guest"))
                return RedirectToAction("Gate", "Entry");

            if (!User.IsInRole("SuperAdmin") && !User.IsInRole("Admin") && !User.IsInRole("Inventory Manager"))
                return RedirectToAction("NoRole", "Entry");

            ItemCategoryModel model = new();
            return View(model);
        }

        [HttpGet("[area]/[controller]/Edit/{Id}")]
        public async Task<IActionResult> Edit(int Id)
        {
            if (User.IsInRole("Guest"))
                return RedirectToAction("Gate", "Entry");

            if (!User.IsInRole("SuperAdmin") && !User.IsInRole("Admin") && !User.IsInRole("Inventory Manager"))
                return RedirectToAction("NoRole", "Entry");

            ItemCategoryModel model = await _Service.GetItemCategory(Id);
            return View(model);
        }

        // =======================
        // Form Actions
        // =======================

        [HttpPost("[area]/[controller]/Insert")]
        public async Task<IActionResult> Insert(ItemCategoryModel model)
        {
            if (!ModelState.IsValid)
                return View("Create", model);

            // --- Pre-check for duplicate Category name (case + trim insensitive)
            var existing = await _Service.GetItemCategory(true); // include inactive to fully enforce uniqueness
            if (existing.Any(x =>
                string.Equals(x.ItemCategoryName?.Trim(), model.ItemCategoryName?.Trim(), StringComparison.OrdinalIgnoreCase)))
            {
                ModelState.AddModelError(nameof(model.ItemCategoryName), "That Category name already exists.");
                return View("Create", model);
            }

            var result = await _Service.InsertItemCategory(model);
            if (!result.IsSuccessful)
            {
                ModelState.AddModelError(string.Empty, string.IsNullOrWhiteSpace(result.Message)
                    ? "Could not create the Category."
                    : result.Message);
                return View("Create", model);
            }

            return RedirectToAction("Index", new { ShowInactive = false });
        }

        [HttpPost("[area]/[controller]/Update")]
        public async Task<IActionResult> Update(ItemCategoryModel model)
        {
            if (!ModelState.IsValid)
                return View("Edit", model);

            await _Service.UpdateItemCategory(model);
            return RedirectToAction("Index", new { ShowInactive = false });
        }

        [HttpPost("[area]/[controller]/Delete/{Id}")]
        public async Task<IActionResult> Delete(int Id)
        {
            await _Service.DeleteItemCategory(Id);
            return RedirectToAction("Index", new { ShowInactive = false });
        }
    }
}
