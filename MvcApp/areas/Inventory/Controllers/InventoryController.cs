using Microsoft.AspNetCore.Mvc;
using MvcApp.Areas.Inventory.Models;
using MvcApp.Services;

namespace MvcApp.Areas.Inventory.Controllers
{
    [Area("Inventory")]
    public class InventoryController(IInventoryService service) : Controller
    {
        private readonly IInventoryService _Service = service;

        // =======================
        // Page Routes (Views)
        // =======================

        [HttpGet("[controller]")]
        public async Task<IActionResult> Index()
        {
            return View();
        }
    }
}
