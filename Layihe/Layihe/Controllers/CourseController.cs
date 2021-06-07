using Layihe.DataAccesLayer;
using Layihe.Models;
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
        public IActionResult Index()
        {
            var courses = _dbContext.Courses.Where(c => c.IsDeleted == false).ToList();
            return View(courses);
        }
        public IActionResult Detail(int? id)
        {
            if (id == null)
                return NotFound();

            var courseDetails = _dbContext.CourseDetails.Where(x => x.IsDeleted == false).Include(x => x.Course).FirstOrDefault(y => y.CourseId == id);

            if (courseDetails == null)
                return NotFound();

            return View(courseDetails);
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
