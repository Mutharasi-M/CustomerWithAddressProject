using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Test.Models;
using Test.ViewModel;

namespace Test.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return RedirectToAction( "CustomerList", "CustomerAddress");
        }


        public IActionResult CustomerForm()
        {
            return View();
        }

        public IActionResult AddressForm(int customerId)
        {

            ViewBag.CustomerId = customerId;

            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}