using Layihe.DataAccesLayer;
using Layihe.Models;
using Layihe.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Layihe.Controllers
{
    public class CourseController : Controller
    {
        private readonly AppDbContext _dbContext;
        public CourseController(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public IActionResult Index(int? categoryId, int page = 1)
        {
            if (categoryId == null)
            {
                
                ViewBag.PageCount = Math.Ceiling((decimal)_dbContext.Courses.Where(x=>x.IsDeleted==false).Count() / 6);
                ViewBag.Page = page;

                if (ViewBag.PageCount < page)
                {
                    return NotFound();
                }
                return View();
            }
            else
            {
                List<Course> courses = new List<Course>();
                List<CourseCategory> courseCategories = _dbContext.CourseCategories.Include(x => x.Course).ToList();
                foreach (CourseCategory courseCategory in courseCategories)
                {
                    if (courseCategory.CategoryId == categoryId && courseCategory.Course.IsDeleted == false)
                    {
                        courses.Add(courseCategory.Course);
                    }
                }

                return View(courses);
            }
        }
        
        #region Detail
        public IActionResult Detail(int? id)
        {
            if (id == null)
                return NotFound();

            var courseDetails = _dbContext.CourseDetails.Where(x => x.IsDeleted == false).Include(x => x.Course).OrderByDescending(t => t.Id)
                                                                                                        .FirstOrDefault(y => y.CourseId == id);

            if (courseDetails == null)
                return NotFound();

            var courseViewModel = new CourseViewModel
            {
                CourseDetail = courseDetails,
                Blogs = _dbContext.Blogs.Where(x => x.IsDeleted == false).Take(3).ToList(),
                Categories = _dbContext.Categories.Include(c => c.CourseCategories).ThenInclude(x => x.Course).ToList()
            };
            return View(courseViewModel);
        }

        #endregion

        #region Search
        public IActionResult Search(string search)
        {
            if (string.IsNullOrEmpty(search))
            {
                return NotFound();
            }

            var courses = _dbContext.Courses.Where(p => p.Name.Contains(search) && p.IsDeleted == false).Take(8)
                                                                                                    .OrderByDescending(p => p.Id).ToList();
            return PartialView("_CourseSearchPartial", courses);
        }

        #endregion
    }
}
