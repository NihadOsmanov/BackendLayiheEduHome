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
        public IActionResult Index(int page = 1)
        {
            ViewBag.PageCount = Decimal.Ceiling((decimal)_dbContext.Courses.Count() / 6);
            ViewBag.Page = page;

            if (ViewBag.PageCount < page)
            {
                return NotFound();
            }

            var courses = _dbContext.Courses.Where(c => c.IsDeleted == false).ToList();
            return View(courses);
        }
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
                Blogs = _dbContext.Blogs.Where(x => x.IsDeleted == false).Take(3).ToList()
            };
            return View(courseViewModel);
        }
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
    }
}
