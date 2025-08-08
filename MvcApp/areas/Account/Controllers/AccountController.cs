using Microsoft.AspNetCore.Mvc;
using MvcApp.Services;

namespace MvcApp.Areas.Account.Controllers
{
    [Area("Account")]
    public class AccountController(IAccountService service) : Controller
    {
        public IAccountService _Service = service;

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