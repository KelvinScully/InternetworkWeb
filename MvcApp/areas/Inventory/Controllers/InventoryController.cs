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
            if (User.IsInRole("Guest"))
                return RedirectToAction("Gate", "Entry");

            if (!User.IsInRole("SuperAdmin") && !User.IsInRole("Admin") && !User.IsInRole("Account Manager"))
                return RedirectToAction("NoRole", "Entry");

            return View();
        }
    }
}
