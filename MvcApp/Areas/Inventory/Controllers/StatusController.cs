using DataAccessLayer.Objects.Inventory;
using Microsoft.AspNetCore.Mvc;
using MvcApp.Areas.Inventory.Models;
using MvcApp.Services;

namespace MvcApp.Areas.Inventory.Controllers
{
    [Area("Inventory")]
    public class StatusController(IInventoryService service) : Controller
    {
        private readonly IInventoryService _Service = service;

        // =======================
        // Page Routes (Views)
        // =======================

        [HttpGet("[area]/[controller]")]
        public async Task<IActionResult> Index()
        {
            var model = await _Service.GetItemStatus();
            return View(model);
        }

        [HttpGet("[area]/[controller]/Create")]
        public async Task<IActionResult> Create()
        {
            ItemStatusModel model = new();
            return View(model);
        }

        [HttpGet("[area]/[controller]/Edit/{Id}")]
        public async Task<IActionResult> Edit(int Id)
        {
            ItemStatusModel model = await _Service.GetItemStatus(Id);
            return View(model);
        }

        // =======================
        // Form Actions
        // =======================

        [HttpPost("[area]/[controller]/Insert")]
        public async Task<IActionResult> Insert(ItemStatusModel model)
        {
            if (!ModelState.IsValid)
                return View("Create", model);

            await _Service.InsertItemStatus(model);
            return RedirectToAction("Index");
        }

        [HttpPost("[area]/[controller]/Update")]
        public async Task<IActionResult> Update(ItemStatusModel model)
        {
            if (!ModelState.IsValid)
                return View("Edit", model);

            await _Service.UpdateItemStatus(model);
            return RedirectToAction("Index");
        }

        [HttpPost("[area]/[controller]/Delete/{Id}")]
        public async Task<IActionResult> Delete(int Id)
        {
            await _Service.DeleteItemStatus(Id);
            return RedirectToAction("Index");
        }
    }
}
