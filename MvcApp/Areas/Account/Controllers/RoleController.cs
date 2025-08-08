using Microsoft.AspNetCore.Mvc;
using MvcApp.Areas.Account.Models;
using MvcApp.Services;

namespace MvcApp.Areas.Account.Controllers
{
    [Area("Account")]
    public class RoleController(IAccountService service) : Controller
    {
        public IAccountService _Service = service;

        // =======================
        // Page Routes (Views)
        // =======================

        [HttpGet("[area]/[controller]/{ShowInactive?}")]
        public async Task<IActionResult> Index(bool ShowInactive = false)
        {
            if (User.IsInRole("Guest"))
                return RedirectToAction("Gate", "Entry");

            var model = await _Service.GetUserRole(ShowInactive);
            return View(model);
        }

        [HttpGet("[area]/[controller]/Create")]
        public IActionResult Create()
        {
            if (User.IsInRole("Guest"))
                return RedirectToAction("Gate", "Entry");

            if (!User.IsInRole("SuperAdmin") && !User.IsInRole("Admin") && !User.IsInRole("Account Manager"))
                return RedirectToAction("NoRole", "Entry");

            return View(new UserRoleModel());
        }

        [HttpGet("[area]/[controller]/Edit/{Id}")]
        public async Task<IActionResult> Edit(int Id)
        {
            if (User.IsInRole("Guest"))
                return RedirectToAction("Gate", "Entry");

            if (!User.IsInRole("SuperAdmin") && !User.IsInRole("Admin") && !User.IsInRole("Account Manager"))
                return RedirectToAction("NoRole", "Entry");

            var model = await _Service.GetUserRole(Id);
            return View(model);
        }

        // =======================
        // Form Actions
        // =======================

        [HttpPost("[area]/[controller]/Insert")]
        public async Task<IActionResult> Insert(UserRoleModel model)
        {
            if (!ModelState.IsValid)
                return View("Create", model);

            await _Service.InsertUserRole(model);
            return RedirectToAction("Index");
        }

        [HttpPost("[area]/[controller]/Update")]
        public async Task<IActionResult> Update(UserRoleModel model)
        {
            if (!ModelState.IsValid)
                return View("Edit", model);

            await _Service.UpdateUserRole(model);
            return RedirectToAction("Index");
        }

        [HttpPost("[area]/[controller]/Delete/{Id}")]
        public async Task<IActionResult> Delete(int Id)
        {
            await _Service.DeleteUserRole(Id);
            return RedirectToAction("Index");
        }
    }
}
