using Layihe.DataAccesLayer;
using Layihe.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Layihe.ViewComponents
{
    public class NoticeVideoViewComponent : ViewComponent
    {
        private readonly AppDbContext _dbContext;

        public NoticeVideoViewComponent(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var video = await _dbContext.Videos.FirstOrDefaultAsync();
            var noticeBoard = await _dbContext.NoticeBoards.Take(6).ToListAsync();

            var homeViewModel = new HomeViewModel
            {
                Video = video,
                NoticeBoards = noticeBoard  
            };
            return View(homeViewModel);
        }
    }
}
