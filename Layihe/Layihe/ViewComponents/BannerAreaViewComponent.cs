using Layihe.DataAccesLayer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Layihe.ViewComponents
{
    public class BannerAreaViewComponent : ViewComponent
    {
        private readonly AppDbContext _dbContext;
        public BannerAreaViewComponent(AppDbContext dbContext)
        {
            _dbContext = dbContext;

        }
        public async Task<IViewComponentResult> InvokeAsync(string title, string source)
        {
            ViewBag.BannerArea = title;
            ViewBag.Source = source;

            var bannerArea = await _dbContext.BannerAreas.FirstOrDefaultAsync();
            return View(bannerArea);
        }
    }
}
