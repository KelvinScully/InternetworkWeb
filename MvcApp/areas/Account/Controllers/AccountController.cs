using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity.UI.V4.Pages.Account.Internal;
using Microsoft.AspNetCore.Mvc;
using MvcApp.areas.Account.Model;
using System.Text.Json;

namespace MvcApp.Areas.Account.Controllers
{
    [Area("Account")]
    [Route("Account")]
    public class AccountController : Controller
    {
        // =======================
        // Page Routes (Views)
        // =======================

        // Dashboard for logged-in users
        [HttpGet("")]
        public IActionResult Index()
        {
            if (!User.Identity?.IsAuthenticated ?? false)
                return RedirectToAction("Gate");

            return View();
        }

        // Login/Register modals
        [HttpGet("Gate")]
        [AllowAnonymous]
        public IActionResult Gate()
        {
            return View();
        }

        // =======================
        // Form Actions
        // =======================

        [HttpPost("Login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromForm] string payload)
        {
            if (string.IsNullOrWhiteSpace(payload)) return RedirectToAction("Gate");
            LoginRegisterModel? model;
            try
            {
                model = JsonSerializer.Deserialize<LoginRegisterModel>(payload);
            }
            catch
            {
                TempData["LoginError"] = "Invalid Login information.";
                return RedirectToAction("Gate");
            }

            if (model == null || model.IsNullOrEmptyForLogin()) return RedirectToAction("Gate");

            // Proceed with auth logic
            TempData["LoginData"] = JsonSerializer.Serialize(model);
            return RedirectToAction("Gate");
        }

        [HttpPost("Register")]
        [AllowAnonymous]
        public async Task<IActionResult> Register([FromForm] string payload)
        {
            if (string.IsNullOrWhiteSpace(payload))
                return RedirectToAction("Gate");

            LoginRegisterModel? model;
            try
            {
                model = JsonSerializer.Deserialize<LoginRegisterModel>(payload);
            }
            catch
            {
                TempData["RegistrationError"] = "Invalid Registration information.";
                return RedirectToAction("Gate");
            }

            if (model == null || model.IsNullOrEmptyForRegistration())
                return RedirectToAction("Gate");

            // Proceed with auth logic
            return RedirectToAction("Index");
        }
    }
}
