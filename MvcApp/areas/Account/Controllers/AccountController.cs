using Microsoft.AspNetCore.Mvc;

namespace MvcApp.Areas.Account.Controllers
{
    [Area("Account")]
    public class AccountController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
