using Layihe.Areas.AdminPanel.Utils;
using Layihe.DataAccesLayer;
using Layihe.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Layihe.Areas.AdminPanel.Controllers
{
    [Area("AdminPanel")]
    [Authorize(Roles = "CourseModerator")]
    public class CourseModeratorController : Controller
    {
        private readonly AppDbContext _dbContext;
        private readonly UserManager<User> _userManager;
        public readonly IWebHostEnvironment _enviroment;

        public CourseModeratorController(AppDbContext dbContext, IWebHostEnvironment enviroment, UserManager<User> userManager)
        {
            _dbContext = dbContext;
            _enviroment = enviroment;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {

            User user = await _userManager.FindByNameAsync(User.Identity.Name);
            var courses = _dbContext.Courses.Where(x => x.IsDeleted == false && x.UserId == user.Id).Include(x => x.CourseDetail).ToList();
            return View(courses);
        }

        #region Update
        public IActionResult Update(int? id)
        {
            ViewBag.Categories = _dbContext.Categories.Where(x => x.IsDeleted == false).ToList();

            if (id == null)
                return NotFound();

            var courseDetail = _dbContext.CourseDetails.Where(x => x.IsDeleted == false).Include(x => x.Course).ThenInclude(t => t.CourseCategories)
                                                                            .ThenInclude(t => t.Category).FirstOrDefault(y => y.CourseId == id);

            if (courseDetail == null)
                return NotFound();

            return View(courseDetail);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(int? id, Course course, List<int?> CategoriesId)
        {
            ViewBag.Categories = _dbContext.Categories.Where(x => x.IsDeleted == false).ToList();

            if (!ModelState.IsValid)
            {
                return View();
            }

            var dbCourse = _dbContext.CourseDetails.Where(x => x.IsDeleted == false).Include(x => x.Course).ThenInclude(t => t.CourseCategories)
                                                                            .ThenInclude(t => t.Category).FirstOrDefault(y => y.CourseId == id);

            if (dbCourse == null)
                return NotFound();


            if (CategoriesId == null)
            {
                ModelState.AddModelError("", "Please select Category");
                return View();
            }

            if (course.Photo != null)
            {
                if (!course.Photo.IsImage())
                {
                    ModelState.AddModelError("Photo", "Not the image you uploaded");
                    return View();
                }

                if (!course.Photo.IsSizeAllowed(1024))
                {
                    ModelState.AddModelError("Photo", "The size of the uploaded image is high");
                    return View();
                }

                var path = Path.Combine(Constants.CourseImageFolderPath, dbCourse.Course.Image);

                if (System.IO.File.Exists(path))
                {
                    System.IO.File.Delete(path);
                }

                var fileName = await FileUtil.GenerateFileAsync(Constants.CourseImageFolderPath, course.Photo);
                dbCourse.Course.Image = fileName;
            }

            if (course.CourseDetail.StartDate < DateTime.Now)
            {
                ModelState.AddModelError("", "Please DateTime correct choose");
                return View();
            }

            var courseCategories = new List<CourseCategory>();

            foreach (var ec in CategoriesId)
            {
                var courseCategory = new CourseCategory();
                courseCategory.CourseId = course.Id;
                courseCategory.CategoryId = (int)ec;
                courseCategories.Add(courseCategory);
            }

            dbCourse.Course.Name = course.Name;
            dbCourse.Course.Description = course.Description;
            dbCourse.AboutCourse = course.CourseDetail.AboutCourse;
            dbCourse.HowToApply = course.CourseDetail.HowToApply;
            dbCourse.Certification = course.CourseDetail.Certification;
            dbCourse.Assesments = course.CourseDetail.Assesments;
            dbCourse.ClassDuration = course.CourseDetail.ClassDuration;
            dbCourse.Duration = course.CourseDetail.Duration;
            dbCourse.StartDate = course.CourseDetail.StartDate;
            dbCourse.Students = course.CourseDetail.Students;
            dbCourse.Language = course.CourseDetail.Language;
            dbCourse.Price = course.CourseDetail.Price;
            dbCourse.Course.CourseCategories = courseCategories;

            await _dbContext.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        #endregion

    }
}
