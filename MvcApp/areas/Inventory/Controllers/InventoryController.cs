using Microsoft.AspNetCore.Mvc;

namespace MvcApp.Areas.Inventory.Controllers
{
    [Area("Inventory")]
    public class InventoryController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }

}
