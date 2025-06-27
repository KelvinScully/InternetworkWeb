using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity.UI.V4.Pages.Account.Internal;
using Microsoft.AspNetCore.Mvc;
using MvcApp.areas.Account.Model;
using MvcApp.Services;
using System.Net.NetworkInformation;
using System.Security.Claims;
using System.Text.Json;

namespace MvcApp.Areas.Account.Controllers
{
    [Area("Account")]
    [Route("Account")]
    public class AccountController : Controller
    {
        public AccountService _AccountService;
        public AccountController(AccountService accountService)
        {
            _AccountService = accountService;
        }

        // =======================
        // Page Routes (Views)
        // =======================

        // Dashboard for logged-in users
        [HttpGet("")]
        public IActionResult Index()
        {
            if (User.IsInRole("Guest"))
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


            var user = await _AccountService.Login(model);

            if (user.IsDefault())
            {
                TempData["LoginError"] = "Login Failed.";
                return RedirectToAction("Gate");
            }

            return RedirectToAction("Index");
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


            var user = await _AccountService.Register(model);

            if (user.IsDefault())
            {
                TempData["LoginError"] = "Registation Failed.";
                return RedirectToAction("Gate");
            }

            return RedirectToAction("Index");
        }
    }
}
