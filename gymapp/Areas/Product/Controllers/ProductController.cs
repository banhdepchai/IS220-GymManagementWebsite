using App.Data;
using App.Models;
using App.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Xml.Linq;
using System;
using ProductModel = App.Models.Products.Product;

namespace App.Areas.Product.Controllers
{
    [Area("Product")]
    [Route("admin/product/[action]/{id?}")]
    [Authorize(Roles = RoleName.Administrator + "," + RoleName.Editor)]
    public class ProductController : Controller
    {
        private readonly GymAppDbContext _context;
        private readonly UserManager<AppUser> _userManager;

        public ProductController(GymAppDbContext context, UserManager<AppUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [TempData]
        public string? StatusMessage { get; set; }

        // GET: Product/Product
        public async Task<IActionResult> Index([FromQuery(Name = "p")] int currentPage, int pagesize)
        {
            var products = _context.Products
                        .Include(p => p.Author)
                        .OrderByDescending(p => p.DateUpdated);

            int totalProducts = await products.CountAsync();
            if (pagesize <= 0) pagesize = 10;
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
                }) ?? string.Empty
            };

            ViewBag.pagingModel = pagingModel;
            ViewBag.totalPosts = totalProducts;

            ViewBag.postIndex = (currentPage - 1) * pagesize;

            var productsInPage = await products.Skip((currentPage - 1) * pagesize)
                             .Take(pagesize)
                             .Include(pc => pc.Category)
                             .ToListAsync();

            return View(productsInPage);
        }

        // GET: Prodcut/Details/id
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products
                .Include(p => p.Author)
                .FirstOrDefaultAsync(m => m.ProductID == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        public async Task<IActionResult> Create()
        {
            ViewData["categories"] = new SelectList(await _context.Categories.ToListAsync(), "Id", "Title");

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ProductName,Description,Content,Price,CategoryID")] ProductModel product)
        {
            var categories = await _context.Categories.ToListAsync();
            ViewData["categories"] = new SelectList(categories, "Id", "Title");

            if (ModelState.IsValid)
            {
                var user = await _userManager.GetUserAsync(this.User);
                product.DateCreated = product.DateUpdated = DateTime.Now;
                product.AuthorId = user.Id;
                _context.Add(product);

                await _context.SaveChangesAsync();
                StatusMessage = "Vừa tạo sản phẩm mới";
                return RedirectToAction(nameof(Index));
            }

            return View(product);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            var productEdit = new ProductModel()
            {
                ProductID = product.ProductID,
                ProductName = product.ProductName,
                Content = product.Content,
                Description = product.Description,
                CategoryID = product.ProductID
            };

            ViewData["categories"] = new SelectList(await _context.Categories.ToListAsync(), "Id", "Title");

            return View(productEdit);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ProductID,ProductName,Description,Content,CategoryID")] ProductModel product)
        {
            if (id != product.ProductID)
            {
                return NotFound();
            }

            ViewData["categories"] = new SelectList(await _context.Categories.ToListAsync(), "Id", "Title");

            if (ModelState.IsValid)
            {
                try
                {
                    var postUpdate = await _context.Products.FindAsync(id);
                    if (postUpdate == null)
                    {
                        return NotFound();
                    }

                    postUpdate.ProductName = product.ProductName;
                    postUpdate.Description = product.Description;
                    postUpdate.Content = product.Content;
                    postUpdate.DateUpdated = DateTime.Now;
                    postUpdate.CategoryID = product.CategoryID;

                    _context.Update(postUpdate);

                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PostExists(product.ProductID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                StatusMessage = "Vừa cập nhật sản phẩm";
                return RedirectToAction(nameof(Index));
            }
            ViewData["AuthorId"] = new SelectList(_context.Users, "Id", "Id", product.AuthorId);
            return View(product);
        }

        // GET: Blog/Post/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var post = await _context.Products
                .Include(p => p.Author)
                .FirstOrDefaultAsync(m => m.ProductID == id);
            if (post == null)
            {
                return NotFound();
            }

            return View(post);
        }

        // POST: Blog/Post/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var product = await _context.Products.FindAsync(id);

            if (product == null)
            {
                return NotFound();
            }

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();

            StatusMessage = "Bạn vừa xóa sản phẩm: " + product.ProductName;

            return RedirectToAction(nameof(Index));
        }

        private bool PostExists(int id)
        {
            return _context.Products.Any(e => e.ProductID == id);
        }
    }
}