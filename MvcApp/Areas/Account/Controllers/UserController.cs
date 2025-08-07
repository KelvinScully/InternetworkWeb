using Microsoft.AspNetCore.Mvc;
using MvcApp.Areas.Account.Models;
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

        [HttpGet("[area]/[controller]/{ShowInactive?}")]
        public async Task<IActionResult> Index(bool ShowInactive = false)
        {
            var model = await _Service.GetUser(ShowInactive);
            return View(model);
        }

        [HttpGet("[area]/[controller]/Create")]
        public IActionResult Create()
        {
            return View(new UserModel());
        }

        [HttpGet("[area]/[controller]/Edit/{Id}")]
        public async Task<IActionResult> Edit(int Id)
        {
            var model = await _Service.GetUser(Id);
            return View(model);
        }

        // === NEW: GET AssignRoles ===

        [HttpGet]
        public async Task<IActionResult> Assign(int Id)
        {
            var user = await _Service.GetUser(Id);
            var allRoles = await _Service.GetUserRole(false);

            var vm = new AssignRolesModel
            {
                UserId = user.UserId,
                UserName = user.UserName,
                ShowInactive = false,
                SelectedRoleId = user.UserRoles.FirstOrDefault()?.UserRoleId
            };

            // only build the list of all active roles
            foreach (var r in allRoles)
            {
                vm.Roles.Add(new AssignRolesModel.RoleItem
                {
                    UserRoleId = r.UserRoleId,
                    UserRoleName = r.UserRoleName
                });
            }

            return View(vm);
        }

        // === NEW: POST AssignRoles ===
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Assign(AssignRolesModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            // 1) Remove _all_ roles currently assigned
            var currentRoles = (await _Service.GetUser(model.UserId)).UserRoles
                                           .Select(r => r.UserRoleId);
            foreach (var rid in currentRoles)
                await _Service.DeleteUserNUserRole(model.UserId, rid);

            // 2) Insert the one selected (if any)
            if (model.SelectedRoleId.HasValue && model.SelectedRoleId.Value > 0)
                await _Service.InsertUserNUserRole(model.UserId, model.SelectedRoleId.Value);

            return RedirectToAction(nameof(Index));
        }

        // =======================
        // Form Actions
        // =======================

        [HttpPost("[area]/[controller]/Insert")]
        public async Task<IActionResult> Insert(UserModel model)
        {
            if (!ModelState.IsValid)
                return View("Create", model);

            await _Service.InsertUser(model);
            return RedirectToAction("Index");
        }

        [HttpPost("[area]/[controller]/Update")]
        public async Task<IActionResult> Update(UserModel model)
        {
            if (!ModelState.IsValid)
                return View("Edit", model);

            await _Service.UpdateUser(model);
            return RedirectToAction("Index");
        }

        [HttpPost("[area]/[controller]/Delete/{Id}")]
        public async Task<IActionResult> Delete(int Id)
        {
            await _Service.DeleteUser(Id);
            return RedirectToAction("Index");
        }
    }
}
