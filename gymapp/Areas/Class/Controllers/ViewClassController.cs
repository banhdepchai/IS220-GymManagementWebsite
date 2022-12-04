using App.Areas.Product.Service;
using App.Models;
using App.Models.Classes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace App.Areas.Class.Controllers
{
    [Area("Class")]
    [AllowAnonymous]
    public class ViewClassController : Controller
    {
        private readonly GymAppDbContext _context;
        private readonly UserManager<AppUser> _userManager;

        public ViewClassController(GymAppDbContext context, UserManager<AppUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [Route("/khoa-tap/")]
        public IActionResult Index([FromQuery(Name = "p")] int currentPage, int pagesize)
        {
            var classes = _context.Classes.Include(i => i.Instructor).AsNoTracking();
            var user = _userManager.GetUserAsync(User).Result;

            int totalClasses = classes.Count();
            if (pagesize <= 0) pagesize = 9;
            int countPages = (int)Math.Ceiling((double)totalClasses / pagesize);

            if (currentPage > countPages) currentPage = countPages;
            if (currentPage < 1) currentPage = 1;

            var pagingModel = new PagingModel()
            {
                countpages = countPages,
                currentpage = currentPage,
                generateUrl = (pageNumber) => Url.Action("Index", new
                {
                    p = pageNumber,
                    pagesize = pagesize
                })
            };

            var classesInPage = classes.Skip((currentPage - 1) * pagesize)
                .Take(pagesize);

            ViewBag.pagingModel = pagingModel;
            ViewBag.totalProducts = totalClasses;
            ViewBag.user = user;

            return View(classesInPage.ToList());
        }
    }
}