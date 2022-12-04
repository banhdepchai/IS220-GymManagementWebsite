using App.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using App.Data;
using Microsoft.AspNetCore.Identity;

namespace App.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly UserManager<AppUser> _userManager;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            //if (User.Identity.IsAuthenticated && User.IsInRole(RoleName.Administrator))
            //{
            //    return RedirectToAction("Index", "AdminCP", new { area = "AdminCP" });
            //}
            //else if (User.Identity.IsAuthenticated && User.IsInRole(RoleName.Member))
            //{
            //    return RedirectToAction("Index", "AdminCP", new { area = "AdminCP" });
            //}
            //else
            {
                return View();
            }
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