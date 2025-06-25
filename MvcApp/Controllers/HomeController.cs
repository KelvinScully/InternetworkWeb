using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MvcApp.Models;
using Repository.Interfaces;

namespace MvcApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ITestRepository _testRepository;

        public HomeController(ITestRepository testRepo)
        {
            _testRepository = testRepo;
        }

        public async Task<IActionResult> Index()
        {
            var result = await _testRepository.TestItemGet(1);
            return View(result.Value);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
