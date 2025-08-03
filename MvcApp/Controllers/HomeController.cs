using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MvcApp.Models;
using MvcApp.Services;
using System.Diagnostics;

namespace MvcApp.Controllers
{
    public class HomeController : Controller
    {
        private IInventoryService _Service;
        private IMapper _Mapper;

        public HomeController(
            IInventoryService service,
            IMapper mapper)
        {
            _Service = service;
            _Mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            var items = await _Service.GetitemStatus(1);
            return View(items);
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
