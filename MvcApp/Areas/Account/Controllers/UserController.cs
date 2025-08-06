using Microsoft.AspNetCore.Mvc;
using MvcApp.Areas.Inventory.Models;
using MvcApp.Services;

namespace MvcApp.Areas.Account.Controllers
{
    [Area("Account")]
    public class UserController(IAccountService service) : Controller
    {
        public IAccountService _Service = service;

        // =======================
        // Page Routes (Views)
        // =======================

        [HttpGet("[area]/[controller]")]
        public async Task<IActionResult> Index()
        {
            return View();
        }

        [HttpGet("[area]/[controller]/Create")]
        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpGet("[area]/[controller]/Edit/{Id}")]
        public async Task<IActionResult> Edit(int Id)
        {
            return View();
        }

        // =======================
        // Form Actions
        // =======================

        [HttpPost("[area]/[controller]/Insert")]
        public async Task<IActionResult> Insert(ItemStatusModel model)
        {
            if (!ModelState.IsValid)
                return View("Create", model);

            return RedirectToAction("Index");
        }

        [HttpPost("[area]/[controller]/Update")]
        public async Task<IActionResult> Update(ItemStatusModel model)
        {
            if (!ModelState.IsValid)
                return View("Edit", model);

            return RedirectToAction("Index");
        }

        [HttpPost("[area]/[controller]/Delete/{Id}")]
        public async Task<IActionResult> Delete(int Id)
        {
            return RedirectToAction("Index");
        }
    }
}
