using AspNetCoreHero.ToastNotification.Abstractions;
using DocumentFormat.OpenXml.Spreadsheet;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using JDshop.Helper;
using JDshop.Models;

namespace JDshop.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin, Nhân Viên")]
    public class ProductsController : Controller
    {
        private readonly JDshopDbContext _context;
        public INotyfService _notyfService { get; }
        public static string? image;
        public ProductsController(JDshopDbContext context, INotyfService notyfService)
        {
            _context = context;
            _notyfService = notyfService;
        }
        [Route("/Admin/product")]
        public async Task<IActionResult> Index()
        {
            var JDshopDbContext = _context.Products.Include(p => p.ProductType).Include(p => p.Category).Include(x => x.Images);
            //var result = _context.Products.Include(p => p.Category).Include(x => x.Images);
            return View(await JDshopDbContext.ToListAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products
                .Include(p => p.ProductType)
                .Include(p => p.Images)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        public IActionResult Create()
        {
            ViewData["ProductTypeId"] = new SelectList(_context.ProductTypes.Where(x => x.Status == 1), "Id", "Name");
            ViewData["CategoryId"] = new SelectList(_context.Categories.Where(x => x.Status == true), "Id", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Product product, List<IFormFile> fAvatars)
        {
            if (_context.Products.Any(p => p.Name == product.Name))
            {
                _notyfService.Error("Tên sản phẩm đã tồn tại.");
                return View(product);
            }
            if (fAvatars.Count == 0)
            {
                _notyfService.Error("Vui lòng chọn ảnh ");
                return View(product);
            }


            _context.Add(product);
            await _context.SaveChangesAsync();
            var ListImage = new List<Image>();
            int i = 1;
            foreach (var file in fAvatars)
            {
                var imagemodel = new Image();
                if (file.Length > 0)
                {
                    string extennsion = Path.GetExtension(file.FileName);
                    image = Utilities.ToUrlFriendly(product.Name + i) + extennsion;
                    imagemodel.Url = await Utilities.UploadFile(file, @"Product", image.ToLower());
                    imagemodel.ProductId = product.Id;
                    imagemodel.Status = true;
                }
                i++;
                ListImage.Add(imagemodel);
            }
            _context.AddRange(ListImage);
            await _context.SaveChangesAsync();
            _notyfService.Success("Thêm sản phẩm thành công.");
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products.Include(x => x.Images).FirstOrDefaultAsync(x => x.Id == id);
            if (product == null)
            {
                return NotFound();
            }
            ViewData["ProductTypeId"] = new SelectList(_context.ProductTypes.Where(x => x.Status == 1), "Id", "Name", product.ProductTypeId);
            ViewData["CategoryId"] = new SelectList(_context.Categories.Where(x => x.Status == true), "Id", "Name", product.CategoryId);
            return View(product);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Product product, List<IFormFile> images)
        {
            if (id != product.Id)
            {
                return NotFound();
            }

            if (_context.Products.Any(p => p.Name == product.Name && p.Id != id))
            {
                _notyfService.Error("Tên sản phẩm đã tồn tại.");
                ViewData["ProductTypeId"] = new SelectList(_context.ProductTypes.Where(x => x.Status == 1), "Id", "Name", product.ProductTypeId);
                ViewData["CategoryId"] = new SelectList(_context.Categories.Where(x => x.Status == true), "Id", "Name", product.CategoryId);
                return View(product);
            }

            if (ModelState.IsValid)
            {
                try
                {

                    if (images.Count() > 0)
                    {
                        var imageDB = _context.Images.Where(x => x.ProductId == id).ToList();
                        _context.RemoveRange(imageDB);
                        await _context.SaveChangesAsync();
                        var ListImage = new List<Image>();
                        int i = 1;
                        foreach (var file in images)
                        {
                            var imagemodel = new Image();
                            if (file.Length > 0)
                            {
                                string extennsion = Path.GetExtension(file.FileName);
                                image = Utilities.ToUrlFriendly(product.Name + i) + extennsion;
                                imagemodel.Url = await Utilities.UploadFile(file, @"Product", image.ToLower());
                                imagemodel.ProductId = product.Id;
                                imagemodel.Status = true;
                            }
                            i++;
                            ListImage.Add(imagemodel);
                        }
                        _context.AddRange(ListImage);


                    }
                    _context.Update(product);
                    await _context.SaveChangesAsync();
                    _notyfService.Success("Cập nhật sản phẩm thành công.");
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductExists(product.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["ProductTypeId"] = new SelectList(_context.ProductTypes.Where(x => x.Status == 1), "Id", "Name", product.ProductTypeId);
            ViewData["CategoryId"] = new SelectList(_context.Categories.Where(x => x.Status == true), "Id", "Name", product.CategoryId);
            return View(product);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products
                .Include(p => p.ProductType)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var product = await _context.Products
                .Include(x => x.Images)
                .Include(x => x.ProductSizeColors)
                .Include(x => x.CollectionProducts)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (product != null)
            {
                // 1. Xoá ảnh sản phẩm
                if (product.Images.Any())
                {
                    _context.Images.RemoveRange(product.Images);
                }

                // 2. Xoá liên kết bộ sưu tập
                if (product.CollectionProducts.Any())
                {
                    _context.CollectionProducts.RemoveRange(product.CollectionProducts);
                }

                // 3. Xử lý từng ProductSizeColor
                foreach (var psc in product.ProductSizeColors)
                {
                    // Xoá trong Order_Items
                    var orderItems = _context.OrderItems
                        .Where(oi => oi.ProductSizeColorId == psc.Id);
                    _context.OrderItems.RemoveRange(orderItems);

                    // Xoá trong Receipt_Products
                    var receipts = _context.ReceiptProducts
                        .Where(rp => rp.ProductSizeColorId == psc.Id);
                    _context.ReceiptProducts.RemoveRange(receipts);

                    // Xoá ProductSizeColor
                    _context.ProductSizeColors.Remove(psc);
                }

                // 4. Cuối cùng, xoá sản phẩm
                _context.Products.Remove(product);

                // 5. Lưu thay đổi
                await _context.SaveChangesAsync();
                _notyfService.Success("Xóa sản phẩm và tất cả dữ liệu liên quan thành công.");
            }

            return RedirectToAction(nameof(Index));
        }


        private bool ProductExists(int id)
        {
            return _context.Products.Any(e => e.Id == id);
        }
    }
}

