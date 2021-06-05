using Layihe.DataAccesLayer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Layihe.ViewComponents
{
    public class CourseViewComponent : ViewComponent
    {
        private readonly AppDbContext _dbContext;

        public CourseViewComponent(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<IViewComponentResult> InvokeAsync(int? take)
        {
            if (take == null)
            {
                var courses = await _dbContext.Courses.Where(d => d.IsDeleted == false).ToListAsync();
                return View(courses);
            }
            else
            {
                var courses = await _dbContext.Courses.Where(d => d.IsDeleted == false).Take((int)take).ToListAsync();
                return View(courses);
            }
        }
    }
}
