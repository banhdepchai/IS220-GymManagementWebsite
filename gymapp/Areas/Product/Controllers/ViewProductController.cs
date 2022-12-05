using App.Areas.Product.Models;
using App.Areas.Product.Service;
using App.Models;
using App.Models.Payments;
using App.Models.Products;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace gymapp.Areas.Product.Controllers
{
    [Area("Product")]
    [Authorize]
    public class ViewProductController : Controller
    {
        private readonly GymAppDbContext _context;
        private readonly CartService _cartService;
        private readonly UserManager<AppUser> _userManager;

        public ViewProductController(GymAppDbContext context, CartService cartService, UserManager<AppUser> userManager)
        {
            _context = context;
            _cartService = cartService;
            _userManager = userManager;
        }

        [Route("/san-pham/")]
        public IActionResult Index([FromQuery(Name = "p")] int currentPage, int pagesize)
        {
            var products = _context.Products.Include(p => p.Category).Include(p => p.ProductPhotos).AsNoTracking();
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

        /// Thêm sản phẩm vào cart
        [Route("addcart/{productid:int}", Name = "addcart")]
        public IActionResult AddToCart([FromRoute] int productid)
        {
            var product = _context.Products
                .Where(p => p.ProductID == productid)
                .Include(p => p.Category)
                .FirstOrDefault();

            if (product == null)
                return NotFound("Không có sản phẩm");

            // Xử lý đưa vào Cart ...
            var cart = _cartService.GetCartItems();
            var cartitem = cart.Find(p => p.product.ProductID == productid);
            if (cartitem != null)
            {
                // Đã tồn tại, tăng thêm 1
                cartitem.quantity++;
            }
            else
            {
                //  Thêm mới
                cart.Add(new CartItem() { quantity = 1, product = product });
            }

            // Lưu cart vào Session
            _cartService.SaveCartSession(cart);
            // Chuyển đến trang hiện thị Cart
            return RedirectToAction(nameof(Cart));
        }

        [Route("/updatecart", Name = "updatecart")]
        [HttpPost]
        public IActionResult UpdateCart([FromForm] int productid, [FromForm] int quantity)
        {
            // Cập nhật Cart thay đổi số lượng quantity ...
            var cart = _cartService.GetCartItems();
            var cartitem = cart.Find(p => p.product.ProductID == productid);
            if (cartitem != null)
            {
                // Đã tồn tại, tăng thêm 1
                cartitem.quantity = quantity;
            }
            _cartService.SaveCartSession(cart);
            // Trả về mã thành công (không có nội dung gì - chỉ để Ajax gọi)
            return Ok();
        }

        /// xóa item trong cart
        [Route("/removecart/{productid:int}", Name = "removecart")]
        public IActionResult RemoveCart([FromRoute] int productid)
        {
            var cart = _cartService.GetCartItems();
            var cartitem = cart.Find(p => p.product.ProductID == productid);
            if (cartitem != null)
            {
                // Đã tồn tại, tăng thêm 1
                cart.Remove(cartitem);
            }

            _cartService.SaveCartSession(cart);
            return RedirectToAction(nameof(Cart));
        }

        [Route("/gio-hang", Name = "cart")]
        public IActionResult Cart()
        {
            List<Discount> discounts = _context.Discounts.ToList();
            ViewBag.discounts = discounts;
            return View(_cartService.GetCartItems());
        }

        [Route("/chi-tiet-don-hang", Name = "payment")]
        public IActionResult Payment()
        {
            decimal total = 0;
            var cart = _cartService.GetCartItems();
            foreach (var item in cart)
            {
                total += item.product.Price * item.quantity;
            }

            var user = _userManager.GetUserAsync(User).Result;

            ViewBag.total = total;
            ViewBag.user = user;

            return View();
        }

        [Route("/xac-nhan-don-hang", Name = "checkout")]
        public async Task<IActionResult> Checkout()
        {
            var cart = _cartService.GetCartItems();

            if (cart.Count == 0)
                return RedirectToAction(nameof(Cart));

            var user = await _userManager.GetUserAsync(this.User);
            var payment = new Payment()
            {
                DateCreated = DateTime.Now,
                UserID = user.Id,
                TotalPrice = cart.Sum(p => p.product.Price * p.quantity)
            };

            _context.Payments.Add(payment);
            _context.SaveChanges();

            foreach (var item in cart)
            {
                var paymentDetail = new PaymentDetail()
                {
                    PaymentID = payment.PaymentID,
                    ProductID = item.product.ProductID,
                    Quantity = item.quantity
                };
                _context.PaymentDetails.Add(paymentDetail);
            }
            _context.SaveChanges();

            _cartService.ClearCart();

            return Content("Xác nhận đơn hàng thành công");
        }
    }
}