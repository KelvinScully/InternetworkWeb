using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MvcApp.Areas.Account.Models;
using MvcApp.Services;
using System.Text.Json;

namespace MvcApp.Areas.Account.Controllers
{
    [Route("Entry")]
    public class EntryController(IAccountService service) : Controller
    {
        public IAccountService _Service = service;

        // =======================
        // Page Routes (Views)
        // =======================

        // Dashboard for logged-in users
        [HttpGet("")]
        public IActionResult Index()
        {
            if (User.IsInRole("Guest"))
                return RedirectToAction("Gate");

            return RedirectToAction("Index", "Home");
        }

        // Login/Register modals
        [HttpGet("Gate")]
        [AllowAnonymous]
        public IActionResult Gate()
        {
            return View();
        }

        [HttpGet("NoRole")]
        [AllowAnonymous]
        public IActionResult NoRole()
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


            var user = await _Service.Authenticate(model);

            if (user.IsDefault())
            {
                TempData["LoginError"] = "Login Failed.";
                return RedirectToAction("Gate");
            }

            return RedirectToAction("Index", "Home");
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


            var user = await _Service.Register(model);

            if (user.IsDefault())
            {
                TempData["LoginError"] = "Registation Failed.";
                return RedirectToAction("Gate");
            }

            return RedirectToAction("Index", "Home");
        }

        [HttpPost("Logout")]
        public async Task<IActionResult> Logout()
        {
            await _Service.SignOutUserAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}