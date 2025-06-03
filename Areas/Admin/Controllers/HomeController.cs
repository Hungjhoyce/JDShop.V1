using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using JDshop.Models;

namespace JDshop.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin, Nhân Viên")]
    public class HomeController : Controller
    {
        private JDshopDbContext _context; public static string? image;
        public INotyfService _notyfService { get; }
        private readonly IConfiguration _configuration;
        public HomeController(JDshopDbContext repo, INotyfService notyfService, IConfiguration configuration)
        {
            _context = repo;
            _notyfService = notyfService;
            _configuration = configuration;
        }
        [Route("/admin")]
        public async Task<IActionResult> Index()
        {
            ///truy vấn dữ liệu 

            var donhuy = await _context.Orders.Where(x => x.Status == 5).ToArrayAsync();
            var User = await _context.Accounts.Where(x => x.RoleId == 3).ToArrayAsync();
            var DonDoanhThu = await _context.Orders.Where(x => x.Status != 1 && x.Status != 5).ToListAsync();
            var sanpham = await _context.Products.Where(x => x.Status == 1).ToListAsync();
            var Giamgia = await _context.Discounts.Where(x => x.Status == 1).ToListAsync();
            //// tính toán dữ liệu 
            int soluongSP = sanpham.Count();
            decimal? tongTien = DonDoanhThu.Sum(x => x.Total);
            int tongdonhang =  DonDoanhThu.Count() + donhuy.Count();
            int soLuong = User.Count();


            var donHangGanDay = await _context.Orders
                .Include(o => o.Account)
                .Where(o => o.Status != 1) // tránh đơn đã huỷ nếu cần
                .OrderByDescending(o => o.CreateDay)
                .Take(5)
                .ToListAsync();

            ViewBag.DonHangGanDay = donHangGanDay;

            var hoatDongGanDay = new List<string>();

            foreach (var order in donHangGanDay)
            {
                var hoatDong = order.Status switch
                {
                    2 => $"Đơn hàng #{order.Id} đang xử lý",
                    3 => $"Đơn hàng #{order.Id} đã giao thành công",
                    4 => $"Đơn hàng #{order.Id} đang giao",
                    5 => $"Đơn hàng #{order.Id} đã bị huỷ",
                    _ => $"Đơn hàng #{order.Id} đang chờ xác nhận"
                };

                hoatDongGanDay.Add(hoatDong);
            }

            ViewBag.HoatDongGanDay = hoatDongGanDay;

            ViewBag.User = soLuong;
            ViewBag.DoanhThu = tongTien;
            ViewBag.Sanpham = soluongSP;
            ViewBag.DonHuy = donhuy.Count();
            ViewBag.TongDonHang = tongdonhang;
            ViewBag.MaGiamGia = Giamgia.Count();
            ViewBag.DonDoanhThu = DonDoanhThu;

            return View();
        }
    }
}
