using Layihe.DataAccesLayer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Layihe.ViewComponents
{
    public class BlogViewComponent : ViewComponent
    {
        private readonly AppDbContext _dbContext;

        public BlogViewComponent(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<IViewComponentResult> InvokeAsync(int? take)
        {
            if (take == 0)
            {
                var blog = await _dbContext.Blogs.Where(x => x.IsDeleted == false).OrderByDescending(y => y.Id).ToListAsync();
                return View(blog);
            }
            else
            {
                var blog = await _dbContext.Blogs.Where(x => x.IsDeleted == false).OrderByDescending(y => y.Id).Take((int)take).ToListAsync();
                return View(blog);
            }
        }
    }
}
