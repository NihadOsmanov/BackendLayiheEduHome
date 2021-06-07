using Layihe.DataAccesLayer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Layihe.Controllers
{
    public class BlogController : Controller
    {
        private readonly AppDbContext _dbContext;

        public BlogController(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IActionResult Index(int page = 1)
        {
            ViewBag.PageCount = Decimal.Ceiling((decimal)_dbContext.Blogs.Count() / 6);
            ViewBag.Page = page;

          
            return View();
        }
        public IActionResult Detail(int? id)
        {
            if (id == null)
                return NotFound();

            var blogDetails = _dbContext.BlogDetails.Where(x => x.IsDelete == false).Include(x => x.Blog).FirstOrDefault(x => x.BlogId == id);
            if (blogDetails == null)
                return NotFound();

            return View(blogDetails);
        }
        public IActionResult Search(string search)
        {
            if (search == null)
                return NotFound();

            var blogs = _dbContext.Blogs.Where(x => x.Title.Contains(search)).Take(8).OrderByDescending(x => x.Id).ToList();

            return PartialView("_BlogSearchPartial", blogs);

        }
    }
}
