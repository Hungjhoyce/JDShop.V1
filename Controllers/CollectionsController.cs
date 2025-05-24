//using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore;
//using JDshop.Models;

//namespace JDshop.Controllers
//{
//    public class CollectionsController : Controller
//    {
//        private readonly JDshopDbContext _context;

//        public CollectionsController(JDshopDbContext context)
//        {
//            _context = context;
//        }

//        public async Task<IActionResult> Index()
//        {
//            var collection = await _context.Collections.ToListAsync();
//            return View(collection);
//        }
//        public IActionResult Details(int id)
//        {
//            var collection = _context.Collections.Include(x=>x.CollectionProducts)
//                .ThenInclude(x=>x.Product).ThenInclude(x=>x.Images)
//                .Include(x=>x.CollectionProducts).ThenInclude(x=>x.Product)
//                .ThenInclude(x=>x.ProductSizeColors).FirstOrDefault(x=>x.Id == id);

//            return View(collection);
//        }
//    }
//}
// cái mới ở dưới
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using JDshop.Models;

namespace JDshop.Controllers
{
    public class CollectionsController : Controller
    {
        private readonly JDshopDbContext _context;

        public CollectionsController(JDshopDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var collection = await _context.Collections.Where(x => x.Status == true).ToListAsync();
            return View(collection);
        }
        public IActionResult Details(int id)
        {
            var collection = _context.Collections.Include(x => x.CollectionProducts)
                .ThenInclude(x => x.Product).ThenInclude(x => x.Images)
                .Include(x => x.CollectionProducts).ThenInclude(x => x.Product)
                .ThenInclude(x => x.ProductSizeColors).FirstOrDefault(x => x.Id == id);

            return View(collection);
        }
    }
}
