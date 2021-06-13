using Layihe.DataAccesLayer;
using Layihe.Models;
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

        public IActionResult Index(int? categoryId, int page = 1)
        {
            if (categoryId == null)
            {
                ViewBag.PageCount = Math.Ceiling((decimal)_dbContext.Blogs.Count() / 6);
                ViewBag.Page = page;

                if (ViewBag.PageCount < page)
                {
                    return NotFound();
                }
                return View();
            }
            else
            {
                List<Blog> blogs = new List<Blog>();
                List<BlogCategory> blogCategories = _dbContext.BlogCategories.Include(x => x.Blog).ToList();
                foreach (BlogCategory blogCategory in blogCategories)
                {
                    if (blogCategory.CategoryId == categoryId && blogCategory.Blog.IsDeleted == false)
                    {
                        blogs.Add(blogCategory.Blog);
                    }
                }

                return View(blogs);
            }
        }

        #region Detail
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
                Blogs = _dbContext.Blogs.Where(x => x.IsDeleted == false).Take(3).ToList(),
                Categories = _dbContext.Categories.Include(c => c.BlogCategories).ThenInclude(x => x.Blog).ToList()
            };
            return View(blogViewModel);
        }

        #endregion

        #region Search
        public IActionResult Search(string search)
        {
            if (search == null)
                return NotFound();

            var blogs = _dbContext.Blogs.Where(x => x.Title.Contains(search)).Take(8).OrderByDescending(x => x.Id).ToList();

            return PartialView("_BlogSearchPartial", blogs);

        }

        #endregion

    }
}
