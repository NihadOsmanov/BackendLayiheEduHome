using Layihe.DataAccesLayer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Layihe.ViewComponents
{
    public class AboutAreaViewComponent : ViewComponent
    {
        private readonly AppDbContext _dbContext;

        public AboutAreaViewComponent(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var aboutArea = await _dbContext.AboutAreas.FirstOrDefaultAsync();

            return View(aboutArea);
        }
    }
}
