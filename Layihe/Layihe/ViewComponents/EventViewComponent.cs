using Layihe.DataAccesLayer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Layihe.ViewComponents
{
    public class EventViewComponent : ViewComponent
    {
        private readonly AppDbContext _dbContext;

        public EventViewComponent(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<IViewComponentResult> InvokeAsync(int? take,int skip)
        {

            if (take == null)
            {
                var events = await _dbContext.Events.Where(x => x.IsDeleted == false).OrderByDescending(y => y.StartingTime).ToListAsync();
                return View(events);
            }
            else
            {
                var events = await _dbContext.Events.Where(x => x.IsDeleted == false).OrderByDescending(y => y.StartingTime).Skip((skip - 1) * (int)take)
                                                                                                                      .Take((int)take).ToListAsync();
                return View(events);
            }

        }
    }
}
