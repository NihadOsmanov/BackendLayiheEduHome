using Layihe.Areas.AdminPanel.Utils;
using Layihe.DataAccesLayer;
using Layihe.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Layihe.Areas.AdminPanel.Controllers
{
    [Area("AdminPanel")]
    [Authorize(Roles = RoleConstant.Admin)]
    public class TeacherController : Controller
    {
        private readonly AppDbContext _dbContext;

        public TeacherController(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IActionResult Index(int page = 1)
        {
            ViewBag.PageCount = Decimal.Ceiling((decimal)_dbContext.Teachers.Where(s => s.IsDeleted == false).Count() / 4);
            ViewBag.Page = page;

            if(ViewBag.PageCount < ViewBag.Page)
            {
                return BadRequest();
            }
            var teachers = _dbContext.Teachers.Where(x => x.IsDeleted == false).Include(x => x.TeacherDetail).Include(x => x.ProfessionOfTeacher)
                                        .OrderByDescending(y => y.Id).Include(x => x.SocialMediaOfTeachers).Skip(((int)page - 1) * 4).Take(4).ToList();

            return View(teachers);
        }

        #region Create
        public async Task<IActionResult> Create()
        {
            ViewBag.Profession = await _dbContext.ProfessionOfTeachers.Where(x => x.IsDeleted == false).ToListAsync();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Teacher teacher, int? ProfessionId)
        {
            ViewBag.Profession = await _dbContext.ProfessionOfTeachers.Where(x => x.IsDeleted == false).ToListAsync();

            if (ProfessionId == null)
            {
                ModelState.AddModelError("", "Please select");
                return View();
            }

            if (!ModelState.IsValid)
            {
                return View();
            }

            if (teacher.Photo == null)
            {
                ModelState.AddModelError("Photo", "Please select Photo");
                return View();
            }

            if (!teacher.Photo.IsImage())
            {
                ModelState.AddModelError("Photo", "Not the image you uploaded");
                return View();
            }

            if (!teacher.Photo.IsSizeAllowed(1024))
            {
                ModelState.AddModelError("Photo", "The size of the uploaded image is high");
                return View();
            }

            var fileName = await FileUtil.GenerateFileAsync(Constants.TeacherImageFolderPath, teacher.Photo);
            teacher.Image = fileName;
            teacher.IsDeleted = false;
            teacher.ProfessionOfTeacherId = (int)ProfessionId;
            await _dbContext.Teachers.AddAsync(teacher);
            teacher.TeacherDetail.TeacherId = teacher.Id;
            await _dbContext.TeacherDetails.AddAsync(teacher.TeacherDetail);
            await _dbContext.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        #endregion

        #region Update
        public async Task<IActionResult> Update(int? id)
        {
            if (id == null)
                return View();

            var teacher = _dbContext.Teachers.Where(x => x.IsDeleted == false).Include(x => x.TeacherDetail).Include(y => y.SocialMediaOfTeachers)
                                                                                  .Include(y => y.ProfessionOfTeacher).FirstOrDefault(x => x.Id == id);

            ViewBag.Profession = await _dbContext.ProfessionOfTeachers.Where(x => x.IsDeleted == false).ToListAsync();

            if (teacher == null)
                return NotFound();

            return View(teacher);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Update(int? id, Teacher teacher, int? ProfessionId)
        {
            ViewBag.Profession = await _dbContext.ProfessionOfTeachers.Where(x => x.IsDeleted == false).ToListAsync();

            if (ProfessionId == null)
            {
                ModelState.AddModelError("", "Please select");
                return View();
            }

            if (!ModelState.IsValid)
                return NotFound();

            if (id == null)
                return View();

            var dbTeacher = _dbContext.Teachers.Where(x => x.IsDeleted == false).Include(x => x.TeacherDetail).Include(y => y.SocialMediaOfTeachers)
                                                                                        .Include(y => y.ProfessionOfTeacher).FirstOrDefault(x => x.Id == id);

            if (dbTeacher == null)
                return NotFound();

            if (teacher.Photo != null)
            {
                if (!teacher.Photo.IsImage())
                {
                    ModelState.AddModelError("Photo", "Not the image you uploaded");
                    return View();
                }
                if (!teacher.Photo.IsSizeAllowed(1024))
                {
                    ModelState.AddModelError("Photo", "The size of the uploaded image is high");
                    return View();
                }

                var path = Path.Combine(Constants.TeacherImageFolderPath, dbTeacher.Image);

                if (System.IO.File.Exists(path))
                {
                    System.IO.File.Delete(path);
                }

                var fileName = await FileUtil.GenerateFileAsync(Constants.TeacherImageFolderPath, teacher.Photo);
                dbTeacher.Image = fileName;
            }

            dbTeacher.FullName = teacher.FullName;
            dbTeacher.TeacherDetail.AboutMe = teacher.TeacherDetail.AboutMe;
            dbTeacher.TeacherDetail.Degree = teacher.TeacherDetail.Degree;
            dbTeacher.TeacherDetail.Experience = teacher.TeacherDetail.Experience;
            dbTeacher.TeacherDetail.Hobbies = teacher.TeacherDetail.Hobbies;
            dbTeacher.TeacherDetail.Faculty = teacher.TeacherDetail.Faculty;
            dbTeacher.TeacherDetail.MailMe = teacher.TeacherDetail.MailMe;
            dbTeacher.TeacherDetail.Number = teacher.TeacherDetail.Number;
            dbTeacher.TeacherDetail.Skype = teacher.TeacherDetail.Skype;
            dbTeacher.TeacherDetail.LanguageValue = teacher.TeacherDetail.LanguageValue;
            dbTeacher.TeacherDetail.DesignValue = teacher.TeacherDetail.DesignValue;
            dbTeacher.TeacherDetail.TeamLeaderValue = teacher.TeacherDetail.TeamLeaderValue;
            dbTeacher.TeacherDetail.InnovtionValue = teacher.TeacherDetail.InnovtionValue;
            dbTeacher.TeacherDetail.DevelopmentValue = teacher.TeacherDetail.DevelopmentValue;
            dbTeacher.TeacherDetail.ComunicationValue = teacher.TeacherDetail.ComunicationValue;
            dbTeacher.ProfessionOfTeacherId = (int)ProfessionId;

            await _dbContext.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        #endregion

        #region Detail
        public IActionResult Detail(int? id)
        {
            if (id == null)
                return NotFound();

            var teacher = _dbContext.Teachers.Where(x => x.IsDeleted == false).Include(x => x.TeacherDetail).Include(x => x.ProfessionOfTeacher).
                                                                                        Include(x => x.SocialMediaOfTeachers).FirstOrDefault(x => x.Id == id);

            if (teacher == null)
                return NotFound();

            return View(teacher);
        }

        #endregion

        #region Delete
        public IActionResult Delete(int? id)
        {
            if (id == null)
                return NotFound();

            var teacher = _dbContext.Teachers.Where(x => x.IsDeleted == false).Include(x => x.TeacherDetail).Include(x => x.ProfessionOfTeacher).
                                                                                        Include(x => x.SocialMediaOfTeachers).FirstOrDefault(x => x.Id == id);

            if (teacher == null)
                return NotFound();

            return View(teacher);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Delete")]
        public async Task<IActionResult> DeleteTeacher(int? id)
        {
            if (id == null)
                return NotFound();

            var teacher = _dbContext.Teachers.Where(x => x.IsDeleted == false).Include(x => x.TeacherDetail).Include(x => x.ProfessionOfTeacher).
                                                                                        Include(x => x.SocialMediaOfTeachers).FirstOrDefault(x => x.Id == id);

            if (teacher == null)
                return NotFound();

            teacher.IsDeleted = true;
            teacher.TeacherDetail.IsDeleted = true;
            await _dbContext.SaveChangesAsync();

            return RedirectToAction("Index");
        }
    }

    #endregion

}
