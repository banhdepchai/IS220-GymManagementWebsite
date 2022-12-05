using App.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace App.Areas.Membership.Controllers
{
    [Area("Membership")]
    [AllowAnonymous]
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
    }
}