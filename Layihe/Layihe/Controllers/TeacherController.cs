using Layihe.DataAccesLayer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Layihe.Controllers
{
    public class TeacherController : Controller
    {
        private readonly AppDbContext _dbContext;
        public TeacherController(AppDbContext dbContexxt)
        {
            _dbContext = dbContexxt;
        }
        public IActionResult Index(int page = 1)
        {
            ViewBag.PageCount = Decimal.Ceiling((decimal)_dbContext.Teachers.Count() / 8);
            ViewBag.Page = page;

            return View();
        }
        public IActionResult Detail(int? id)
        {
            if (id == null) 
                return NotFound();

            var teacherDetail = _dbContext.TeacherDetails.Where(x => x.IsDeleted == false).Include(x => x.Teacher).ThenInclude(y => y.SocialMediaOfTeachers)
                                                        .Include(t => t.Teacher).ThenInclude(t => t.ProfessionOfTeacher).FirstOrDefault(z => z.TeacherId == id);

            if (teacherDetail == null) 
                return NotFound();
           
            return View(teacherDetail);
        }
        public IActionResult Search(string search)
        {
            if (search == null)
                return NotFound();

            var teachers = _dbContext.Teachers.Where(x => x.IsDeleted == false).Include(p => p.ProfessionOfTeacher).Include(p => p.SocialMediaOfTeachers)
                                            .Where(y => y.FullName.Contains(search)).Take(8).OrderByDescending(y => y.Id).ToList();
            return PartialView("_TeacherSearchPartial", teachers);
        }
    }
}
