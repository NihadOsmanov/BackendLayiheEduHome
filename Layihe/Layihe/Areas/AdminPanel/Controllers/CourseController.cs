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

            if (ViewBag.PageCount < page)
            {
                return NotFound();
            }

            var courses = _dbContext.Courses.Where(x => x.IsDeleted == false).Include(x => x.CourseDetail).OrderByDescending(x => x.Id)
                                                                                            .Skip(((int)page -1) * 4).Take(4).ToList();
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

            return View(courseDetails);
        }
        public IActionResult Create()
        {
            return View();
        }

            [HttpPost]
            [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Course course)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            if (course.Photo == null)
            {
                ModelState.AddModelError("Photo", "Please select Photo");
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

            //var isExist = _dbContext.Courses.Where(x => x.IsDeleted == false).Any(x => x.Name.ToLower() == course.Name.ToLower());
            //if (isExist)
            //{
            //    ModelState.AddModelError("Name", "There is a course with this name");
            //    return View();
            //}

            var fileName = await FileUtil.GenerateFileAsync(Constants.CourseImageFolderPath, course.Photo);

            course.Image = fileName;
            course.IsDeleted = false;

            await _dbContext.Courses.AddAsync(course);
            course.CourseDetail.CourseId = course.Id;
            await _dbContext.CourseDetails.AddAsync(course.CourseDetail);
            await _dbContext.SaveChangesAsync();

            return RedirectToAction("Index");
        }
        public IActionResult Update(int? id)
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
        public async Task<IActionResult> Update(int? id, Course course)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            var dbCourse = _dbContext.CourseDetails.Where(x => x.IsDeleted == false).Include(x => x.Course).OrderByDescending(t => t.Id)
                                                                                                        .FirstOrDefault(y => y.CourseId == id);

            if (dbCourse == null)
                return NotFound();

            if(course.Photo != null)
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

            await _dbContext.SaveChangesAsync();

            return RedirectToAction("Index");
        }
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

            //var path = Path.Combine(Constants.ImageFolderPath, courseDetails.Course.Image);
            //if (System.IO.File.Exists(path))
            //{
            //    System.IO.File.Delete(path);
            //}

           
            courseDetail.IsDeleted = true;
            courseDetail.Course.IsDeleted = true;
            await _dbContext.SaveChangesAsync();

            return RedirectToAction("Index");
        }
    }
}
