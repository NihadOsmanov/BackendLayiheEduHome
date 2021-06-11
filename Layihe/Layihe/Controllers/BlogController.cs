using Layihe.DataAccesLayer;
using Layihe.ViewModels;
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

            if (ViewBag.PageCount < page)
            {
                return NotFound();
            }

            return View();
        }
        public IActionResult Detail(int? id)
        {
            if (id == null)
                return NotFound();

            var blogDetails = _dbContext.BlogDetails.Where(x => x.IsDelete == false).Include(x => x.Blog).OrderByDescending(x => x.Id).FirstOrDefault(x => x.BlogId == id);
            if (blogDetails == null)
                return NotFound();

            var blogViewModel = new BlogViewModel
            {
                BlogDetail = blogDetails,
                Blogs = _dbContext.Blogs.Where(x => x.IsDeleted == false).Take(3).ToList()
            };
            return View(blogViewModel);
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
