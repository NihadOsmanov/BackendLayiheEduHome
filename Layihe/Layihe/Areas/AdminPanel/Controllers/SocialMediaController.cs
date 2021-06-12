using Layihe.DataAccesLayer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Layihe.Areas.AdminPanel.Controllers
{
    [Area("AdminPanel")]
    public class SocialMediaController : Controller
    {
        private readonly AppDbContext _dbContext;

        public SocialMediaController(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IActionResult Index(int page = 1)
        {
            ViewBag.PageCount = Decimal.Ceiling((decimal)_dbContext.Teachers.Where(s => s.IsDeleted == false).Count() / 4);
            ViewBag.Page = page;

            if (ViewBag.PageCount < page)
            {
                return NotFound();
            }

            var teachers = _dbContext.Teachers.Where(x => x.IsDeleted == false).Include(x => x.TeacherDetail).Include(x => x.ProfessionOfTeacher)
                                        .OrderByDescending(y => y.Id).Include(x => x.SocialMediaOfTeachers).Skip(((int)page - 1) * 4).Take(4).ToList();

            return View(teachers);
        }
        public IActionResult Create()
        {
            return View();
        }
    }
}
