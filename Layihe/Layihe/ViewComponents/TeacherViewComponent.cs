using Layihe.DataAccesLayer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Layihe.ViewComponents
{
    public class TeacherViewComponent : ViewComponent
    {
        private readonly AppDbContext _dbContext;

        public TeacherViewComponent(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<IViewComponentResult> InvokeAsync(int? take, int skip)
        {

            if (take == null)
            {
                var teacher = await _dbContext.Teachers.Where(x => x.IsDeleted == false).Include(y => y.SocialMediaOfTeachers).Include(z => z.ProfessionOfTeacher).ToListAsync();
                return View(teacher);
            }
            else
            {
                var teacher = await _dbContext.Teachers.Where(x => x.IsDeleted == false).Include(y => y.SocialMediaOfTeachers)
                                                        .Include(z => z.ProfessionOfTeacher).Skip((skip - 1) * 6).Take((int)take).ToListAsync();
                return View(teacher);
            }

        }
    }
}
