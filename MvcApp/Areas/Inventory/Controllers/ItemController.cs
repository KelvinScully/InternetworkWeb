using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MvcApp.Areas.Inventory.Models;
using MvcApp.Services;

namespace MvcApp.Areas.Inventory.Controllers
{
    [Area("Inventory")]
    public class ItemController : Controller
    {
        private readonly IInventoryService _Service;

        public ItemController(IInventoryService service)
        {
            _Service = service;
        }

        private async Task LoadDropdownsAsync()
        {
            // TEMPORARY hardcoded test data
            var categories = new[]
            {
                new { CategoryId = 1, ItemCategoryName = "Electronics" },
                new { CategoryId = 2, ItemCategoryName = "Furniture" },
                new { CategoryId = 3, ItemCategoryName = "Office Supplies" }
            };

            var locations = new[]
            {
                new { LocationId = 1, ItemLocationName = "Main Warehouse" },
                new { LocationId = 2, ItemLocationName = "Front Office" },
                new { LocationId = 2, ItemLocationName = "Storage Room" }
            };

            var statuses = new[]
            {
                new { StatusId = 4, ItemStatusName = "Available" },
                new { StatusId = 5, ItemStatusName = "Out of Stock" }
            };

            ViewData["Categories"] = new SelectList(categories, "CategoryId", "ItemCategoryName");
            ViewData["Locations"] = new SelectList(locations, "LocationId", "ItemLocationName");
            ViewData["Statuses"] = new SelectList(statuses, "StatusId", "ItemStatusName");
        }



        [HttpGet("[area]/[controller]")]
        public async Task<IActionResult> Index()
        {
            var items = await _Service.GetItem(false);
            return View(items);
        }

        [HttpGet("[area]/[controller]/Create")]
        public async Task<IActionResult> Create()
        {
            await LoadDropdownsAsync();
            return View(new ItemModel { ItemId = 0 });
        }

        [HttpPost("[area]/[controller]/Insert")]
        public async Task<IActionResult> Insert(ItemModel model)
        {
            if (!ModelState.IsValid)
            {
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

            return RedirectToAction("Index");
        }

        [HttpGet("[area]/[controller]/Edit/{Id}")]
        public async Task<IActionResult> Edit(int Id)
        {
            var item = await _Service.GetItem(Id);
            await LoadDropdownsAsync();
            return View(item);
        }

        [HttpPost("[area]/[controller]/Update")]
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

            return RedirectToAction("Index");
        }
    }
}
