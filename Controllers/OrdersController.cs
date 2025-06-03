using AspNetCoreHero.ToastNotification.Abstractions;
using MailKit.Security;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MimeKit;
using OfficeOpenXml.FormulaParsing.Excel.Functions.RefAndLookup;
using JDshop.Models;
using JDshop.Models.ViewModel.ViewModel;
using JDshop.Services;

namespace JDshop.Controllers
{
    public class OrdersController : Controller
    {
        private readonly JDshopDbContext _context;
        public INotyfService _notyfService { get; }  private readonly IVnPayService _vnPayService;
        public OrdersController(JDshopDbContext context, INotyfService notyfService,IVnPayService vnPayService)
        {

            _context = context;
            _notyfService = notyfService;
           _vnPayService = vnPayService;
        }
        public IActionResult Index()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> AddToCart(int productId, string size, string color, int quantity)
        {
            try
            {
                var makhclaim = User.Claims.FirstOrDefault(c => c.Type == "Id");

                if (makhclaim == null)
                {
                    return Json(new { success = false, message = "Vui lòng đăng nhập" });
                }

                var maKH = makhclaim.Value;

                var productColorSize = _context.ProductSizeColors.Include(x => x.Size)
                    .Include(x => x.Color).FirstOrDefault(x => x.ProductId == productId && x.Color.Color1 == color && x.Size.Size1 == size);
                if (productColorSize == null)
                {
                    return Json(new { success = false, message = "Sản phẩm không tồn tại" });
                }
                var dondathang = await _context.Orders.FirstOrDefaultAsync(x => x.AccountId == int.Parse(maKH) && x.Status == 1);

                if (dondathang == null)
                {
                    dondathang = new Order
                    {
                        AccountId = int.Parse(maKH),
                        Status = 1,

                    };
                    _context.Orders.Add(dondathang);
                    await _context.SaveChangesAsync();
                }

                var chitietdonthang = await _context.OrderItems.FirstOrDefaultAsync(x => x.OrderId == dondathang.Id && x.ProductSizeColorId == productColorSize.Id);

                if (chitietdonthang == null)
                {
                    chitietdonthang = new OrderItem
                    {
                        OrderId = dondathang.Id,
                        ProductSizeColorId = productColorSize.Id,
                        Quantity = quantity,
                    };

                    _context.OrderItems.Add(chitietdonthang);
                    await _context.SaveChangesAsync();

                }
                else
                {
                    // Sách đã có trong chi tiết đơn hàng, tăng số lượng lên một đơn vị
                    chitietdonthang.Quantity++;
                    await _context.SaveChangesAsync();
                }
                _notyfService.Success("Thêm sản phẩm thành công ");

                return Json(new { success = true, message = "Sản phẩm đã được thêm vào giỏ hàng." });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Lỗi: " + ex.Message });
            }
        }

        public async Task<IActionResult> UpdateOrder(int id)
        {
            var makhclaim = User.Claims.FirstOrDefault(c => c.Type == "Id");
            if (makhclaim == null)
            {
                _notyfService.Error("Vui lòng đăng nhập");
                return RedirectToAction("Login", "Accounts");
            }
            var maKH = int.Parse(makhclaim.Value);

            var order = await _context.Orders
                .FirstOrDefaultAsync(x => x.AccountId == maKH && x.Status == 1);

            if (order == null)
            {
                _notyfService.Error("Không tìm thấy đơn hàng");
                return RedirectToAction("Index", "Home");
            }

            var orderItem = await _context.OrderItems
                .FirstOrDefaultAsync(x => x.OrderId == order.Id && x.ProductSizeColorId == id);

            if (orderItem == null)
            {
                _notyfService.Error("Không tìm thấy sản phẩm trong giỏ hàng");
                return RedirectToAction("ShoppingCart", "Products");
            }

            // Giảm số lượng
            orderItem.Quantity--;

            // Nếu số lượng = 0, xóa item khỏi giỏ hàng
            if (orderItem.Quantity <= 0)
            {
                _context.OrderItems.Remove(orderItem);
                _notyfService.Success("Đã xóa sản phẩm khỏi giỏ hàng");
            }
            else
            {
                _notyfService.Success("Đã cập nhật số lượng");
            }

            await _context.SaveChangesAsync();
            return RedirectToAction("ShoppingCart", "Products");
        }
        public IActionResult UpdateOrderAdd(int? id)
        {
            var makhclaim = User.Claims.FirstOrDefault(c => c.Type == "Id");
            if (makhclaim == null)
            {
                _notyfService.Error("Vui lòng đăng nhập trước khi mua hàng");
                return RedirectToAction("Index", "Home");
            }
            var maKH = makhclaim.Value;

            // Truy vấn đơn hàng chưa hoàn thành
            var dondathang = _context.OrderItems
                .Include(x => x.Oder)
                .Where(x => x.Oder.AccountId == int.Parse(maKH) && x.Oder.Status == 1);

            OrderItem? sanpham = dondathang.FirstOrDefault(x => x.ProductSizeColorId == id);


            if (sanpham != null)
            {
                sanpham.Quantity = sanpham.Quantity + 1;
                _context.SaveChanges(); // Lưu thay đổi số lượng vào cơ sở dữ liệu
            }
            return RedirectToAction("ShoppingCart", "Products");
        }
        public IActionResult RemoveCart()
        {
            var makhclaim = User.Claims.FirstOrDefault(c => c.Type == "Id");
            if (makhclaim == null)
            {
                _notyfService.Error("Vui lòng đăng nhập trước khi mua hàng");
                return RedirectToAction("Index", "Home");
            }
            var maKH = makhclaim.Value;

            // Truy vấn đơn hàng chưa hoàn thành
            var dondathang = _context.OrderItems
                .Include(x => x.Oder)
                .Where(x => x.Oder.AccountId == int.Parse(maKH)
                && x.Oder.Status == 1);
            // Xóa hết các mục trong danh sách đơn đặt hàng
            _context.OrderItems.RemoveRange(dondathang);
            _context.SaveChanges();
            _notyfService.Success("Xóa giỏ hàng thành công");
            return RedirectToAction("Index", "Home");
        }

        public IActionResult RemoveProduct(int id)
        {
            var makhclaim = User.Claims.FirstOrDefault(c => c.Type == "Id");
            if (makhclaim == null)
            {
                _notyfService.Error("Vui lòng đăng nhập trước khi mua hàng");
                return RedirectToAction("Index", "Home");
            }
            var maKH = makhclaim.Value;

            // Truy vấn đơn hàng chưa hoàn thành
            var dondathang = _context.OrderItems
                .Include(x => x.Oder)
                .Where(x => x.Oder.AccountId == int.Parse(maKH) && x.Oder.Status == 1);

            OrderItem? sanpham = dondathang.FirstOrDefault(x => x.ProductSizeColorId == id);

            if (sanpham != null)
            {
                _context.OrderItems.Remove(sanpham);
                _context.SaveChanges();
                return RedirectToAction("ShoppingCart", "Products");
            }
            if (dondathang == null)
            {
                return RedirectToAction("Index", "Home");
            }
            return RedirectToAction("ShoppingCart", "Products");
        }


        public async Task<IActionResult> CheckOut(int pay, int Address, decimal total)
        {
            var makhclaim = User.Claims.FirstOrDefault(c => c.Type == "Id");

            if (makhclaim == null)
            {
                _notyfService.Error("Vui lòng đăng nhập trước khi mua hàng");
                return RedirectToAction("Index", "Home");
            }
            var maKH = makhclaim.Value;
            var giohang = await _context.Orders
                .Include(x => x.Account).ThenInclude(x => x.Addresses)
                .Include(x => x.Discount)
                .FirstOrDefaultAsync(x => x.AccountId == int.Parse(maKH) && x.Status == 1);

            if (giohang == null)
            {
                _notyfService.Warning("Bạn chưa có sản phẩm nào trong giỏ hàng");
                return RedirectToAction("Index", "Home");
            }

            if (giohang.Discount != null)
            {
                giohang.Discount.UseNumber = (giohang.Discount.UseNumber ?? 0) + 1;
                
                if (giohang.Discount.UseNumber >= giohang.Discount.Quantity)
                {
                    giohang.Discount.Status = 2;
                }
            }

            var orderItems = _context.OrderItems
                .Include(x => x.ProductSizeColor)
                    .ThenInclude(x => x.ProductInventory)
                .Include(x => x.ProductSizeColor)
                    .ThenInclude(x => x.Product)
                .Include(x => x.ProductSizeColor)
                    .ThenInclude(x => x.Color)
                .Include(x => x.ProductSizeColor)
                    .ThenInclude(x => x.Size)
                .Where(x => x.OrderId == giohang.Id)
                .ToList();

            foreach (var item in orderItems)
            {
                if ((item.ProductSizeColor.ProductInventory.QuantitySold + item.Quantity) > item.ProductSizeColor.ProductInventory.Quantity)
                {
                    var soluongsp = item.ProductSizeColor.ProductInventory.Quantity - item.ProductSizeColor.ProductInventory.QuantitySold;
                    _notyfService.Error($"{item.ProductSizeColor.Product.Name} chỉ còn {soluongsp} sản phẩm");
                    return RedirectToAction("ShoppingCart", "Products");
                }

                item.ProductSizeColor.ProductInventory.QuantitySold += item.Quantity;
            }
            await _context.SaveChangesAsync();



            giohang.Status = 2;
            giohang.CreateDay = DateTime.Now;
            giohang.AddressId = Address;
            giohang.Total = total;

            Payment payment = new Payment
            {
                OrdersId = giohang.Id,
                PaymentMethodsId = pay,
                Amount = giohang.Total,
                PaymentDate = DateTime.Now,
                Status = 1
            };
            _context.Payments.Add(payment);
            await _context.SaveChangesAsync();

            if (pay == 2)
            {
                var VnPayModel = new VnPaymentRequestModel
                {
                    Amount =(double)(giohang.Total),
                    CreatedDate = DateTime.Now,
                    Description = $"{giohang.Account.FullName}",
                    FullName = giohang.Account.FullName,
                    OrderId = giohang.Id

                };
                return Redirect(_vnPayService.CreatePaymentUrl(HttpContext, VnPayModel));

            }

            _notyfService.Success("Đặt hàng thành công");
            var address = _context.Addresses.FirstOrDefault(x => x.Id == Address);

// Gửi email thông báo đặt hàng thành công
var email = giohang.Account.Email;
var emailSubject = "Đặt hàng thành công";

// Tạo bảng HTML chứa thông tin sản phẩm
string productTable = "<table border='1' cellpadding='5' cellspacing='0' style='border-collapse:collapse; width:100%'>" +
                      "<tr style='background-color: #f8f9fa;'>" +
                      "<th>Sản phẩm</th>" +
                      "<th>Kích cỡ</th>" +
                      "<th>Màu sắc</th>" +
                      "<th>Số lượng</th>" +
                      "<th>Đơn giá</th>" +
                      "<th>Thành tiền</th></tr>";

foreach (var item in orderItems)
{
    var product = item.ProductSizeColor.Product;
    var color = item.ProductSizeColor.Color?.Color1 ?? "N/A";
    var size = item.ProductSizeColor.Size?.Size1 ?? "N/A";
    var quantity = item.Quantity;
    var unitPrice = item.ProductSizeColor.Product?.Price;
    var totalPrice = unitPrice * quantity;

    productTable += $"<tr>" +
                    $"<td>{product.Name}</td>" +
                    $"<td>{size}</td>" +
                    $"<td>{color}</td>" +
                    $"<td style='text-align: center;'>{quantity}</td>" +
                    $"<td style='text-align: right;'>{unitPrice:N0} VNĐ</td>" +
                    $"<td style='text-align: right;'>{totalPrice:N0} VNĐ</td>" +
                    $"</tr>";
}
productTable += "</table>";

var emailBody = $@"
    <div style='font-family: Arial, sans-serif; max-width: 600px; margin: 0 auto;'>
        <h2 style='color: #333;'>Cảm ơn bạn đã đặt hàng!</h2>
        <p>Đơn hàng của bạn đã được đặt thành công với thông tin như sau:</p>
        
        <div style='background-color: #f8f9fa; padding: 15px; border-radius: 5px; margin: 15px 0;'>
            <p><strong>Mã đơn hàng:</strong> #{giohang.Id}</p>
            <p><strong>Tổng cộng:</strong> {giohang.Total:N0} VNĐ</p>
            <p><strong>Địa chỉ giao hàng:</strong><br/> 
            {address.Street}, {address.Ward}, {address.District}, {address.City}, {address.Country}</p>
        </div>

        <h3 style='color: #333; margin-top: 20px;'>Chi tiết sản phẩm:</h3>
        {productTable}
        <br/>
        <p>Chúng tôi sẽ xử lý đơn hàng của bạn trong thời gian sớm nhất.</p>
        <p style='color: #666;'>Trân trọng,<br/>Đội ngũ hỗ trợ khách hàng</p>
    </div>";

await SendEmailAsync(email, emailSubject, emailBody);
 

            return RedirectToAction("Index", "Home");
        }

        // Phương thức gửi email
        private async Task SendEmailAsync(string email, string subject, string body)
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("AdminDotnet", "admin@example.com"));
            message.To.Add(MailboxAddress.Parse(email));
            message.Subject = subject;

            message.Body = new TextPart("html") // Sử dụng định dạng HTML
            {
                Text = body
            };

            using var smtp = new MailKit.Net.Smtp.SmtpClient();
            smtp.Connect("smtp.gmail.com", 587, SecureSocketOptions.StartTls);
            smtp.Authenticate("hung.jhoyce@gmail.com", "getp xtqh muxs saur");
            await smtp.SendAsync(message);
            smtp.Disconnect(true);
        }


          [Authorize]
  public async Task<IActionResult> PaymentCallBack()
  {
      var response = _vnPayService.PaymentExecute(Request.Query);
      if (response == null || response.VnPayResponseCode != "00")
      {
          var makhClaim = User.Claims.FirstOrDefault(c => c.Type == "Id");

          if (makhClaim == null)
          {
              _notyfService.Error("Vui lòng đăng nhập trước khi mua hàng");
              return RedirectToAction("Index", "Home");
          }

          var maKH = makhClaim.Value;
          var giohang = await _context.Orders
              .Include(x => x.Account)
              .Include(x => x.Payments)
              .Include(x => x.Discount)
              .OrderByDescending(x => x.CreateDay)
              .FirstOrDefaultAsync(x => x.AccountId == int.Parse(maKH) && x.Status == 2);

          if (giohang.Discount != null)
          {
              giohang.Discount.UseNumber = Math.Max(0, (giohang.Discount.UseNumber ?? 0) - 1);
              giohang.Discount.Status = 1;
              giohang.DiscountId = null;
          }

          giohang.Total = null;
          giohang.CreateDay = null;
          giohang.AddressId = null;
          giohang.Status = 1;
          _context.Payments.RemoveRange(giohang.Payments);
          await _context.SaveChangesAsync();
          _notyfService.Error($"Lỗi thanh toán VNPAY: {response.VnPayResponseCode}");
          return RedirectToAction("Index", "Home");
      }

      _notyfService.Success("Thanh toán thành công");
      return RedirectToAction("Index", "Home");
  }

        [HttpGet]
        public async Task<IActionResult> ApplyDiscount(string code)
        {
            try
            {
                // Get current user's ID
                var makhclaim = User.Claims.FirstOrDefault(c => c.Type == "Id");
                if (makhclaim == null)
                {
                    return Json(new { success = false, message = "Vui lòng đăng nhập để sử dụng mã giảm giá" });
                }
                var maKH = int.Parse(makhclaim.Value);

                // Get current active order
                var order = await _context.Orders
                    .FirstOrDefaultAsync(x => x.AccountId == maKH && x.Status == 1);

                if (order == null)
                {
                    return Json(new { success = false, message = "Không tìm thấy đơn hàng" });
                }

                // Kiểm tra mã giảm giá có tồn tại và còn hiệu lực không
                var discount = await _context.Discounts
                    .FirstOrDefaultAsync(d => d.Code == code && d.Status == 1);

                if (discount == null)
                {
                    return Json(new { success = false, message = "Mã giảm giá không tồn tại hoặc đã hết hạn" });
                }

                // Kiểm tra số lượng còn lại
                if (discount.Quantity <= discount.UseNumber)
                {
                    return Json(new { success = false, message = "Mã giảm giá đã hết lượt sử dụng" });
                }

                // Apply discount to order
                order.DiscountId = discount.Id;
                await _context.SaveChangesAsync();

                // Trả về thông tin giảm giá
                return Json(new { 
                    success = true, 
                    discountAmount = discount.DiscountPercent,
                    message = "Áp dụng mã giảm giá thành công"
                });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Có lỗi xảy ra: " + ex.Message });
            }
        }
    }
}
