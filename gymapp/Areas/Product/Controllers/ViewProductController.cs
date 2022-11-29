using App.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace gymapp.Areas.Product.Controllers
{
    [Area("Product")]
    [AllowAnonymous]
    public class ViewProductController : Controller
    {
        private readonly GymAppDbContext _context;

        public ViewProductController(GymAppDbContext context)
        {
            _context = context;
        }

        [Route("/san-pham/")]
        public IActionResult Index([FromQuery(Name = "p")] int currentPage, int pagesize)
        {
            var products = _context.Products.Include(p => p.Category);
            var categories = _context.Categories.ToList();

            int totalProducts = products.Count();
            if (pagesize <= 0) pagesize = 9;
            int countPages = (int)Math.Ceiling((double)totalProducts / pagesize);

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

            var productsInPage = products.Skip((currentPage - 1) * pagesize)
                .Take(pagesize);

            ViewBag.categories = categories;
            ViewBag.pagingModel = pagingModel;
            ViewBag.totalProducts = totalProducts;

            return View(productsInPage.ToList());
        }

        [Route("/{slug}.html")]
        public IActionResult Detail(string slug)
        {
            var product = _context.Products.Include(p => p.Category).FirstOrDefault(p => p.Slug == slug);
            if (product == null) return NotFound();

            return View(product);
        }

        public async Task<IActionResult> GetCategories()
        {
            var categories = await _context.Categories.ToListAsync();
            ViewBag["categories"] = categories;
            return View();
        }
    }
}