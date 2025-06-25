using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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
        // Form Actions (Posts)
        // =======================

        [HttpPost("Login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromForm] string payload)
        {
            // TODO: Deserialize and authenticate user
            return RedirectToAction("Index");
        }

        [HttpPost("Register")]
        [AllowAnonymous]
        public async Task<IActionResult> Register([FromForm] string payload)
        {
            // TODO: Deserialize and register user
            return RedirectToAction("Index");
        }
    }
}
