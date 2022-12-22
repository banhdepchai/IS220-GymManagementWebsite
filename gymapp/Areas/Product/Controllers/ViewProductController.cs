using App.Areas.Product.Models;
using App.Areas.Product.Service;
using App.Models;
using App.Models.Payments;
using App.Models.Products;
using BraintreeHttp;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PayPal.Core;
using PayPal.v1.Orders;
using PayPalPayments = PayPal.v1.Payments;

namespace gymapp.Areas.Product.Controllers
{
    [Area("Product")]
    [Authorize]
    public class ViewProductController : Controller
    {
        private readonly GymAppDbContext _context;
        private readonly CartService _cartService;
        private readonly UserManager<AppUser> _userManager;
        private readonly string _clientId;
        private readonly string _secretKey;

        public double TyGiaUSD = 23300;

        public ViewProductController(GymAppDbContext context, CartService cartService, UserManager<AppUser> userManager, IConfiguration config)
        {
            _context = context;
            _cartService = cartService;
            _userManager = userManager;
            _clientId = config["PaypalSettings:ClientId"];
            _secretKey = config["PaypalSettings:SecretKey"];
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

            return View(productsInPage.OrderByDescending(d => d.DateUpdated).ToList());
        }

        [Route("/san-pham/{danhmuc}")]
        public IActionResult GetProductsByCategory([FromQuery(Name = "p")] int currentPage, int pagesize, string danhmuc)
        {
            var products = _context.Products.Include(p => p.Category).Include(p => p.ProductPhotos.OrderBy(pt => pt.Id)).Where(s => s.Category.Slug == danhmuc).AsNoTracking();
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

            if (danhmuc != "")
            {
                var category = _context.Categories.FirstOrDefault(s => s.Slug == danhmuc);

                if (category != null)
                {
                    ViewBag.category = category;
                }
            }
            ViewBag.categories = categories;
            ViewBag.pagingModel = pagingModel;
            ViewBag.totalProducts = totalProducts;

            return View(productsInPage.OrderByDescending(d => d.DateUpdated).ToList());
        }

        [Route("/{slug}.html")]
        public IActionResult Detail(string slug)
        {
            var product = _context.Products
                .Include(p => p.Category)
                .Include(p => p.ProductPhotos.OrderBy(pt => pt.Id))
                .FirstOrDefault(p => p.Slug == slug);
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

            var count = _cartService.GetCartItems().Count();
            return RedirectToAction(nameof(Cart));

            ////return Json(new { success = true, count = count });
        }

        [Route("/gio-hang", Name = "cart")]
        public IActionResult Cart()
        {
            ViewBag.discount = _cartService.GetDiscount();

            return View(_cartService.GetCartItems());
        }

        [Route("/chi-tiet-don-hang", Name = "payment")]
        public IActionResult Payment()
        {
            var cart = _cartService.GetCartItems();

            if (cart.Count == 0)
                return RedirectToAction(nameof(Cart));

            decimal totalOld = 0;
            foreach (var item in cart)
            {
                totalOld += item.product.Price * item.quantity;
            }

            decimal total = totalOld;
            var discountCode = _cartService.GetDiscount();
            int? dicountId = null;

            if (discountCode != null)
            {
                dicountId = discountCode.Id;
                total -= (total * discountCode.Percent / 100);
            }

            var user = _userManager.GetUserAsync(User).Result;

            ViewBag.totalOld = totalOld;
            ViewBag.total = total;
            ViewBag.discountPrice = totalOld - total;
            ViewBag.user = user;

            return View();
        }

        [Route("/xac-nhan-don-hang", Name = "checkout")]
        public async Task<IActionResult> Checkout()
        {
            var cart = _cartService.GetCartItems();

            if (cart.Count == 0)
                return RedirectToAction(nameof(Cart));

            decimal total = 0;
            foreach (var item in cart)
            {
                total += item.product.Price * item.quantity;
            }

            var discountCode = _cartService.GetDiscount();
            int? dicountId = null;
            if (discountCode != null)
            {
                dicountId = discountCode.Id;
                total -= (total * discountCode.Percent / 100);
            }

            var user = await _userManager.GetUserAsync(this.User);
            var payment = new Payment()
            {
                DateCreated = DateTime.Now,
                UserID = user.Id,
                DiscountId = dicountId,
                PaymentMode = "Trả sau",
                TotalPrice = total
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
            //TempData["SuccessMessage"] = "Đặt hàng thành công";
            TempData["StatusMessage"] = "Đặt hàng thành công";
            return RedirectToAction(nameof(Cart));
            //return Content("Xác nhận đơn hàng thành công");
        }

        [Route("/kiem-tra-ma-giam-gia/", Name = "checkdiscount")]
        [HttpPost]
        public IActionResult CheckDiscount(string code)
        {
            var discount = _context.Discounts.FirstOrDefault(p => p.Code == code);
            if (discount == null)
                return Json(new { success = false, message = "Mã giảm giá không tồn tại" });

            var user = _userManager.GetUserAsync(this.User).Result;
            var payment = _context.Payments.Where(u => u.UserID == user.Id).ToList();

            if (payment.Any(p => p.DiscountId == discount.Id))
                return Json(new { success = false, message = "Bạn đã sử dụng mã giảm giá này" });

            _cartService.SaveDiscountSession(discount);

            int discountPercent = discount.Percent;

            return Json(new { success = true, message = "Mã giảm giá hợp lệ", discount = discountPercent });
        }

        [Route("/thanh-toan-paypal", Name = "paypal")]
        public async Task<IActionResult> PayPalCheckout()
        {
            var enviroment = new SandboxEnvironment(_clientId, _secretKey);
            var client = new PayPalHttpClient(enviroment);

            var cart = _cartService.GetCartItems();

            #region Create Paypal Order

            var itemList = new PayPalPayments.ItemList()
            {
                Items = new List<PayPalPayments.Item>()
            };
            var total = Math.Round(cart.Sum(p => (double)p.product.Price * p.quantity) / TyGiaUSD, 2);
            foreach (var item in cart)
            {
                itemList.Items.Add(new PayPalPayments.Item()
                {
                    Name = item.product.ProductName,
                    Currency = "USD",
                    Price = Math.Round((double)item.product.Price / TyGiaUSD, 2).ToString(),
                    Quantity = item.quantity.ToString(),
                    Sku = "sku",
                    Tax = "0"
                });
            }

            #endregion Create Paypal Order

            var paypalOrderId = DateTime.Now.Ticks;
            var hostname = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host}";
            var payment = new PayPal.v1.Payments.Payment()
            {
                Intent = "sale",
                Transactions = new List<PayPalPayments.Transaction>()
                {
                    new PayPalPayments.Transaction()
                    {
                        Amount = new PayPalPayments.Amount()
                        {
                            Total = total.ToString(),
                            Currency = "USD",
                            Details = new PayPalPayments.AmountDetails
                            {
                                Tax = "0",
                                Shipping = "0",
                                Subtotal = total.ToString()
                            }
                        },
                        ItemList = itemList,
                        Description = $"Invoice #{paypalOrderId}",
                        InvoiceNumber = paypalOrderId.ToString()
                    }
                },
                RedirectUrls = new PayPalPayments.RedirectUrls()
                {
                    CancelUrl = $"{hostname}/GioHang/CheckoutFail",
                    ReturnUrl = $"{hostname}/thanh-toan-paypal/thanh-cong"
                },
                Payer = new PayPalPayments.Payer()
                {
                    PaymentMethod = "paypal"
                }
            };

            PayPalPayments.PaymentCreateRequest request = new PayPalPayments.PaymentCreateRequest();
            request.RequestBody(payment);

            try
            {
                var response = await client.Execute(request);
                var statusCode = response.StatusCode;
                PayPal.v1.Payments.Payment result = response.Result<PayPal.v1.Payments.Payment>();

                var links = result.Links.GetEnumerator();
                string paypalRedirectUrl = null;
                while (links.MoveNext())
                {
                    PayPalPayments.LinkDescriptionObject lnk = links.Current;
                    if (lnk.Rel.ToLower().Trim().Equals("approval_url"))
                    {
                        //saving the payapalredirect URL to which user will be redirected for payment
                        paypalRedirectUrl = lnk.Href;
                    }
                }

                return Redirect(paypalRedirectUrl);
            }
            catch (HttpException httpException)
            {
                var statusCode = httpException.StatusCode;
                var debugId = httpException.Headers.GetValues("PayPal-Debug-Id").FirstOrDefault();

                //Process when Checkout with Paypal fails
                return Redirect("/GioHang/CheckoutFail");
            }
        }

        public IActionResult CheckoutFail()
        {
            //return Content("Xác nhận đơn hàng thành công");
            return View();
        }

        [Route("/thanh-toan-paypal/thanh-cong", Name = "paypalsuccess")]
        public async Task<IActionResult> CheckoutSuccess()
        {
            var cart = _cartService.GetCartItems();

            if (cart.Count == 0)
                return RedirectToAction(nameof(Cart));

            var user = await _userManager.GetUserAsync(this.User);
            decimal total = 0;
            foreach (var item in cart)
            {
                total += item.product.Price * item.quantity;
            }
            var discountCode = _cartService.GetDiscount();
            int? dicountId = null;
            if (discountCode != null)
            {
                dicountId = discountCode.Id;
                total -= (total * discountCode.Percent / 100);
            }
            var payment = new Payment()
            {
                DateCreated = DateTime.Now,
                UserID = user.Id,
                TotalPrice = total,
                DiscountId = dicountId,
                PaymentMode = "Paypal",
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
            //TempData["SuccessMessage"] = "Đặt hàng thành công";
            TempData["StatusMessage"] = "Đặt hàng thành công";
            return RedirectToAction(nameof(Cart));
        }
    }
}