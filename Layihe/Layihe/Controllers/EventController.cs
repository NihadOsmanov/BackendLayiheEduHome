using Layihe.DataAccesLayer;
using Layihe.Models;
using Layihe.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Layihe.Controllers
{
    public class EventController : Controller
    {
        private readonly AppDbContext _dbContext;
        public EventController(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public IActionResult Index(int? categoryId, int page = 1)
        {
            if(categoryId == null)
            {
                ViewBag.PageCount = Math.Ceiling((decimal)_dbContext.Events.Where(x => x.IsDeleted == false).Count() / 6);
                ViewBag.Page = page;

                if (ViewBag.PageCount < page)
                {
                    return NotFound();
                }
                return View();
            }
            else
            {
                List<Event> events = new List<Event>();
                List<EventCategory> eventCategories = _dbContext.EventCategories.Include(x => x.Event).ToList();
                foreach (EventCategory eventCategory in eventCategories)
                {
                    if (eventCategory.CategoryId == categoryId && eventCategory.Event.IsDeleted == false)
                    {
                        events.Add(eventCategory.Event);
                    }
                }
                return View(events);
            }
        }

        #region Detail
        public IActionResult Detail(int? id)
        {
            if (id == null)
                return NotFound();

            var eventDetail = _dbContext.EventDetails.Where(x => x.IsDelete == false).Include(x => x.Event).ThenInclude(y => y.EventSpikers).ThenInclude(y => y.Spiker)
                                                                        .OrderByDescending(t => t.Id).FirstOrDefault(z => z.EventId == id);

            if (eventDetail == null)
                return NotFound();

            var eventViewModel = new EventViewModel
            {
                EventDetail = eventDetail,
                Blogs = _dbContext.Blogs.Where(x => x.IsDeleted == false).Take(3).ToList(),
                Categories = _dbContext.Categories.Include(c => c.EventCategories).ThenInclude(x => x.Event).ToList()
            };

            if (eventViewModel.EventDetail == null)
                return NotFound();


            return View(eventViewModel);
        }

        #endregion

        #region Search
        public IActionResult Search(string search)
        {
            if (search == null)
                return NotFound();

            var events = _dbContext.Events.Where(p => p.Name.Contains(search) && p.IsDeleted == false)
                                                            .Take(8).OrderByDescending(p => p.Id).ToList();
            return PartialView("_EventSearchPartial", events);
        }

        #endregion
    }
}
