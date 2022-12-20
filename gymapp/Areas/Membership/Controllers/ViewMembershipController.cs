using App.Models;
using App.Models.Memberships;
using App.Models.Payments;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace App.Areas.Membership.Controllers
{
    [Area("Membership")]
    [Authorize]
    public class ViewMembershipController : Controller
    {
        private readonly GymAppDbContext _context;
        private readonly UserManager<AppUser> _userManager;

        public ViewMembershipController(GymAppDbContext context, UserManager<AppUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [Route("/dich-vu/")]
        public async Task<IActionResult> Index()
        {
            var memberships = await _context.Memberships.ToListAsync();
            var memDefault = await _context.Memberships.FirstOrDefaultAsync();
            var levels = new SelectList(await _context.Memberships.ToListAsync(), "MembershipId", "Level");
            var user = await _userManager.GetUserAsync(User);

            ViewBag.memDefault = memDefault;
            ViewBag.levels = levels;
            ViewBag.user = user;

            return View(memberships);
        }

        [Route("/membership/{id}")]
        public async Task<IActionResult> ShowMembership(int id)
        {
            var membership = _context.Memberships.FirstOrDefault(m => m.MembershipId == id);

            return PartialView("_ShowMembership", membership);
        }

        [HttpGet]
        [Route("/dich-vu/{MembershipId}/xac-nhan-don-hang")]
        public async Task<IActionResult> Checkout(int MembershipId)
        {
            var membership = await _context.Memberships.FirstOrDefaultAsync(m => m.MembershipId == MembershipId);
            var user = await _userManager.GetUserAsync(User);

            ViewBag.membership = membership;
            ViewBag.user = user;

            return View();
        }

        [HttpPost]
        [Route("/dich-vu/{MembershipId}/xac-nhan-don-hang")]
        public async Task<IActionResult> Checkout(int MembershipId, [Bind("TotalPrice,DateCreated,PaymentMode")] Payment payment)
        {
            if (ModelState.IsValid)
            {
                var membership = await _context.Memberships.FirstOrDefaultAsync(m => m.MembershipId == MembershipId);
                var user = await _userManager.GetUserAsync(this.User);
                ViewBag.user = user;
                payment.UserID = user.Id;

                _context.Payments.Add(payment);
                await _context.SaveChangesAsync();

                var SignupMembership = new SignupMembership
                {
                    MembershipId = membership.MembershipId,
                    PaymentId = payment.PaymentID,
                    UserId = user.Id,
                    SignupDate = payment.DateCreated
                };
                _context.SignupMemberships.Add(SignupMembership);
                await _context.SaveChangesAsync();

                TempData["SuccessMessage"] = "Đăng ký thành công";
                return RedirectToAction(nameof(Index));
            }
            return View();
        }
    }
}