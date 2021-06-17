using Layihe.DataAccesLayer;
using Layihe.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Layihe.Areas.AdminPanel.Controllers
{
    [Area("AdminPanel")]
    [Authorize(Roles = RoleConstant.Admin)]
    public class SocialMediaController : Controller
    {
        private readonly AppDbContext _dbContext;

        public SocialMediaController(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IActionResult Index(int page = 1)
        {
            ViewBag.PageCount = Math.Ceiling((decimal)_dbContext.Teachers.Where(s => s.IsDeleted == false).Count() / 4);
            ViewBag.Page = page;

            if (ViewBag.PageCount < page || page <= 0)
            {
                return NotFound();
            }

            var teachers = _dbContext.Teachers.Where(x => x.IsDeleted == false).Include(x => x.TeacherDetail).Include(x => x.ProfessionOfTeacher)
                                                                  .OrderByDescending(y => y.Id).Include(x => x.SocialMediaOfTeachers.
                                                                  Where(y => y.IsDeleted == false)).Skip(((int)page - 1) * 4).Take(4).ToList();

            return View(teachers);
        }

        #region Create
        public IActionResult Create()
        {
            ViewBag.Teachers = _dbContext.Teachers.Where(x => x.IsDeleted == false).ToList();
            ViewBag.SocialMedia = _dbContext.SocialMedias.ToList();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(SocialMediaOfTeacher SocialMediaOfTeacher, int? teachersId)
        {
            var teachers = _dbContext.Teachers.Where(x => x.IsDeleted == false).Include(x => x.SocialMediaOfTeachers).ToList();
            ViewBag.Teachers = teachers;
            ViewBag.SocialMedia = _dbContext.SocialMedias.ToList();

            if (!ModelState.IsValid)
            {
                return View();
            }

            if (teachersId == null)
            {
                ModelState.AddModelError("", "Please select Teacher");
                return View();
            }

            if (teachers.All(x => x.Id != (int)teachersId))
            {
                return BadRequest();
            }

            foreach (var teacher in teachers)
            {
                if (teacher.Id == teachersId)
                {
                    foreach (var item in teacher.SocialMediaOfTeachers)
                    {
                        if (item.IsDeleted == false && item.Link == SocialMediaOfTeacher.Link && item.Icon == SocialMediaOfTeacher.Icon)
                        {
                            ModelState.AddModelError("", "is exists");
                            return View();
                        }
                    }
                }
            }

            SocialMediaOfTeacher.TeacherId = (int)teachersId;
            await _dbContext.AddAsync(SocialMediaOfTeacher);
            await _dbContext.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        #endregion

        #region Update
        public IActionResult Update(int? id)
        {
            if (id == null)
                return NotFound();

            ViewBag.Teachers = _dbContext.Teachers.Where(x => x.IsDeleted == false).ToList();
            ViewBag.SocialMedia = _dbContext.SocialMedias.ToList();

            var dbSocMedOfTeacher = _dbContext.SocialMediaOfTeachers.Where(x => x.IsDeleted == false).Include(x => x.Teacher).FirstOrDefault(x => x.Id == id);

            if (dbSocMedOfTeacher == null)
                return NotFound();

            return View(dbSocMedOfTeacher);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(int? id, SocialMediaOfTeacher SocialMediaOfTeacher)
        {

            if (id == null)
                return NotFound();

            var teachers = _dbContext.Teachers.Where(x => x.IsDeleted == false).Include(x => x.SocialMediaOfTeachers).ToList();
            ViewBag.SocialMedia = _dbContext.SocialMedias.ToList();

            var dbSocMedOfTeacher = _dbContext.SocialMediaOfTeachers.Where(x => x.IsDeleted == false).Include(x => x.Teacher).FirstOrDefault(x => x.Id == id);

            if (dbSocMedOfTeacher == null)
                return NotFound();

            foreach (var teacher in teachers)
            {
                if (teacher.Id != id)
                {
                    foreach (var item in teacher.SocialMediaOfTeachers)
                    {
                        if (item.IsDeleted == false && item.Link == SocialMediaOfTeacher.Link && item.Icon == SocialMediaOfTeacher.Icon)
                        {
                            ModelState.AddModelError("", "is exists");
                            return View(dbSocMedOfTeacher);
                        }
                    }
                }
            }

            dbSocMedOfTeacher.Link = SocialMediaOfTeacher.Link;
            dbSocMedOfTeacher.Icon = SocialMediaOfTeacher.Icon;

            await _dbContext.SaveChangesAsync();

            return RedirectToAction("Index");

        }
        #endregion

        #region Delete
        public IActionResult Delete(int? id)
        {
            if (id == null)
                return NotFound();

            ViewBag.Teachers = _dbContext.Teachers.Where(x => x.IsDeleted == false).ToList();
            ViewBag.SocialMedia = _dbContext.SocialMedias.ToList();

            var dbSocMedOfTeacher = _dbContext.SocialMediaOfTeachers.Where(x => x.IsDeleted == false).Include(x => x.Teacher).FirstOrDefault(x => x.Id == id);

            if (dbSocMedOfTeacher == null)
                return NotFound();

            return View(dbSocMedOfTeacher);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Delete")]
        public async Task<IActionResult> DeleteSocialMedia(int? id)
        {
            if (id == null)
                return NotFound();

            ViewBag.Teachers = _dbContext.Teachers.Where(x => x.IsDeleted == false).ToList();
            ViewBag.SocialMedia = _dbContext.SocialMedias.ToList();

            var dbSocMedOfTeacher = _dbContext.SocialMediaOfTeachers.Where(x => x.IsDeleted == false).Include(x => x.Teacher).FirstOrDefault(x => x.Id == id);

            if (dbSocMedOfTeacher == null)
                return NotFound();

            dbSocMedOfTeacher.IsDeleted = true;
            await _dbContext.SaveChangesAsync();

            return RedirectToAction("Index");
        }
        #endregion

        #region Detail
        public IActionResult Detail(int? id)
        {
            if (id == null)
                return NotFound();

            ViewBag.Teachers = _dbContext.Teachers.Where(x => x.IsDeleted == false).ToList();
            ViewBag.SocialMedia = _dbContext.SocialMedias.ToList();

            var dbSocMedOfTeacher = _dbContext.SocialMediaOfTeachers.Where(x => x.IsDeleted == false).Include(x => x.Teacher).FirstOrDefault(x => x.Id == id);

            if (dbSocMedOfTeacher == null)
                return NotFound();

            return View(dbSocMedOfTeacher);
        }
        #endregion
    }
}
