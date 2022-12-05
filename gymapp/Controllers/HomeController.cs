using App.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using App.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using App.Models.Memberships;

namespace App.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly UserManager<AppUser> _userManager;
        private readonly GymAppDbContext _context;

        public HomeController(ILogger<HomeController> logger, GymAppDbContext context, UserManager<AppUser> userManager)
        {
            _logger = logger;
            _context = context;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var memberships = await _context.Memberships.ToListAsync();

            ViewBag.memberships = memberships;
            return View();
        }

        [Route("/ve-chung-toi")]
        public async Task<IActionResult> About()
        {
            var classes = await _context.Classes.CountAsync();
            var products = await _context.Products.CountAsync();
            var users = await _context.Users.CountAsync();
            var instructors = await _context.Instructors.CountAsync();

            ViewBag.classes = classes;
            ViewBag.products = products;
            ViewBag.users = users;
            ViewBag.instructors = instructors;

            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}