using App.Controllers;
using App.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace App.Areas.Admin.Controllers
{
    [Area("AdminCP")]
    [Authorize(Roles = RoleName.Administrator)]
    public class AdminCPController : Controller
    {
        [Route("/admin/")]
        public IActionResult Index()
        {
            return View();
        }
    }
}