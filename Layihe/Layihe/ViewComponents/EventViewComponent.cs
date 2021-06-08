﻿using Layihe.DataAccesLayer;
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
                var events = _dbContext.Events.Where(x => x.IsDeleted == false).OrderByDescending(y => y.StartingTime).ToList();
                return View(events);
            }
            else
            {
                var events = _dbContext.Events.Where(x => x.IsDeleted == false).OrderByDescending(y => y.StartingTime).Skip((skip - 1) * 6)
                                                                                                                      .Take((int)take).ToList();
                return View(events);
            }

        }
    }
}
