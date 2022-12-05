using App.Areas.Product.Service;
using App.Models;
using App.Models.Classes;
using App.Models.Payments;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace App.Areas.Class.Controllers
{
    [Area("Class")]
    [Authorize]
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
        public async Task<IActionResult> Index([FromQuery(Name = "p")] int currentPage, int pagesize)
        {
            var classes = _context.Classes.Include(i => i.Instructor);
            var user = await _userManager.GetUserAsync(this.User);

            int totalClasses = await _context.Classes.CountAsync();
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

            var classesInPage = await classes.Skip((currentPage - 1) * pagesize)
                .Take(pagesize).ToListAsync();

            ViewBag.pagingModel = pagingModel;
            ViewBag.totalProducts = totalClasses;
            ViewBag.user = user;

            return View(classesInPage);
        }

        [Route("/khoa-tap/{id}")]
        public async Task<IActionResult> GetClass(int id)
        {
            var classModel = await _context.Classes.Include(i => i.Instructor).FirstOrDefaultAsync(i => i.ClassId == id);
            var user = await _userManager.GetUserAsync(this.User);

            ViewBag.user = user;

            return PartialView("_GetClass", classModel);
        }

        [HttpGet]
        [Route("/khoa-tap/{classId}/xac-nhan-don-hang")]
        public async Task<IActionResult> Checkout(int classId)
        {
            var classModel = await _context.Classes.FirstOrDefaultAsync(m => m.ClassId == classId);
            var user = await _userManager.GetUserAsync(User);

            ViewBag.classModel = classModel;
            ViewBag.user = user;

            return View();
        }

        [HttpPost]
        [Route("/khoa-tap/{classId}/xac-nhan-don-hang")]
        public async Task<IActionResult> Checkout(int classId, [Bind("TotalPrice,DateCreated,PaymentMode")] Payment payment)
        {
            if (ModelState.IsValid)
            {
                var classModel = await _context.Classes.FirstOrDefaultAsync(m => m.ClassId == classId);
                var user = await _userManager.GetUserAsync(this.User);
                ViewBag.user = user;
                payment.UserID = user.Id;

                _context.Payments.Add(payment);
                await _context.SaveChangesAsync();

                var signupClass = new SignupClass()
                {
                    ClassId = classId,
                    UserId = user.Id,
                    PaymentId = payment.PaymentID
                };
                _context.SignupClasses.Add(signupClass);
                await _context.SaveChangesAsync();

                return Content($"Đăng ký khóa tập {classModel.ClassTitle} thành công");
            }
            return View();
        }
    }
}