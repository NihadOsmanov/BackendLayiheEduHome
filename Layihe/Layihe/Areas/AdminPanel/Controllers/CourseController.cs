using Layihe.Areas.AdminPanel.Utils;
using Layihe.DataAccesLayer;
using Layihe.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
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
    [Authorize(Roles = RoleConstant.Admin)]
    public class CourseController : Controller
    {
        private readonly AppDbContext _dbContext;
        public readonly IWebHostEnvironment _enviroment;

        public CourseController(AppDbContext dbContext, IWebHostEnvironment enviroment)
        {
            _dbContext = dbContext;
            _enviroment = enviroment;
        }

        public IActionResult Index(int page = 1)
        {
            ViewBag.PageCount = Math.Ceiling((decimal)_dbContext.Courses.Where(s => s.IsDeleted == false).Count() / 4);
            ViewBag.Page = page;

            if (ViewBag.PageCount < page || page <= 0)
            {
                return NotFound();
            }

            var courses = _dbContext.Courses.Where(x => x.IsDeleted == false).Include(x => x.CourseDetail).OrderByDescending(x => x.Id)
                                                                                            .Skip(((int)page -1) * 4).Take(4).ToList();
            return View(courses);
        }

        #region Detail
        public IActionResult Detail(int? id)
        {
            if (id == null)
                return NotFound();

            var courseDetails = _dbContext.CourseDetails.Where(x => x.IsDeleted == false).Include(x => x.Course).ThenInclude(y => y.CourseCategories)
                                                                        .ThenInclude(y => y.Category).FirstOrDefault(y => y.CourseId == id);

            if (courseDetails == null)
                return NotFound();

            return View(courseDetails);
        }

        #endregion

        #region Create
        public IActionResult Create()
        {
            ViewBag.Categories = _dbContext.Categories.Where(x => x.IsDeleted == false).ToList();

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Course course, List<int?> CategoriesId)
        {
            var categories = _dbContext.Categories.Where(x => x.IsDeleted == false).ToList();
            ViewBag.Categories = categories;

            if (!ModelState.IsValid)
            {
                return View();
            }

            if (CategoriesId.Count == 0)
            {
                ModelState.AddModelError("", "Please select Category");
                return View();
            }

            foreach (var category in CategoriesId)
            {
                if (categories.All(x => x.Id != (int)category))
                {
                    return BadRequest();
                }
            }

            if (course.Photo == null)
            {
                ModelState.AddModelError("Photo", "Please select Photo");
                return View();
            }

            if(course.CourseDetail.StartDate < DateTime.Now)
            {
                ModelState.AddModelError("", "Please DateTime correct choose");
                return View();
            }

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

            var fileName = await FileUtil.GenerateFileAsync(Constants.CourseImageFolderPath, course.Photo);

            course.Image = fileName;
            course.IsDeleted = false;

            var courseCategories = new List<CourseCategory>();

            foreach (var ec in CategoriesId)
            {
                var courseCategory = new CourseCategory();
                courseCategory.CourseId = course.Id;
                courseCategory.CategoryId = (int)ec;
                courseCategories.Add(courseCategory);
            }

            if(course.CourseDetail.Students < 0)
            {
                ModelState.AddModelError("", "Stundents can not be less than 0");
                return View();
            }

            if (course.CourseDetail.Price < 0)
            {
                ModelState.AddModelError("", "Price can not be less than 0");
                return View();
            }

            await _dbContext.Courses.AddAsync(course);
            course.CourseDetail.CourseId = course.Id;
            course.CourseCategories = courseCategories;
            await _dbContext.CourseDetails.AddAsync(course.CourseDetail);
            await _dbContext.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        #endregion

        #region Update
        public IActionResult Update(int? id)
        {
            var categories = _dbContext.Categories.Where(x => x.IsDeleted == false).ToList();

            ViewBag.Categories = categories;

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
            var categories = _dbContext.Categories.Where(x => x.IsDeleted == false).ToList();

            ViewBag.Categories = categories;

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

            foreach (var category in CategoriesId)
            {
                if (categories.All(x => x.Id != (int)category))
                {
                    return BadRequest();
                }
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

            if (course.CourseDetail.Students < 0)
            {
                ModelState.AddModelError("", "Stundents can not be less than 0");
                return View();
            }

            if (course.CourseDetail.Price < 0)
            {
                ModelState.AddModelError("", "Price can not be less than 0");
                return View();
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

        #region Delete
        public IActionResult Delete(int? id)
        {
            if (id == null)
                return NotFound();

            var courseDetail = _dbContext.CourseDetails.Where(x => x.IsDeleted == false).Include(x => x.Course).OrderByDescending(t => t.Id)
                                                                                                        .FirstOrDefault(y => y.CourseId == id);

            if (courseDetail == null)
                return NotFound();

            return View(courseDetail);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Delete")]
        public async Task<IActionResult> DeleteCourse(int? id)
        {
            if (id == null)
                return NotFound();

            var courseDetail = _dbContext.CourseDetails.Where(x => x.IsDeleted == false).Include(x => x.Course).OrderByDescending(t => t.Id)
                                                                                                        .FirstOrDefault(y => y.CourseId == id);

            if (courseDetail == null)
                return NotFound();

            courseDetail.IsDeleted = true;
            courseDetail.Course.IsDeleted = true;
            await _dbContext.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        #endregion

    }
}
