using Layihe.DataAccesLayer;
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
        public IActionResult Index(int page = 1)
        {
            ViewBag.PageCount = Decimal.Ceiling((decimal)_dbContext.Events.Count() / 6);
            ViewBag.Page = page;

            return View();
        }
        public IActionResult Detail(int? id)
        {
            if (id == null)
                return NotFound();

            var eventDetail = _dbContext.EventDetails.Where(x => x.IsDelete == false).Include(x => x.Event).ThenInclude(y => y.EventSpikers).ThenInclude(y => y.Spiker)
                                                                        .OrderByDescending(t => t.Id).FirstOrDefault(z => z.EventId == id);

            var eventViewModel = new EventViewModel
            {
                EventDetail = eventDetail,
                Blogs = _dbContext.Blogs.Where(x => x.IsDeleted == false).Take(3).ToList()
            };

            if (eventDetail == null)
                return NotFound();

            return View(eventViewModel);
        }
        public IActionResult Search(string search)
        {
            if (search == null) 
                return NotFound();

            var events = _dbContext.Events.Where(p => p.Name.Contains(search) && p.IsDeleted == false)
                                                            .Take(8).OrderByDescending(p => p.Id).ToList();
            return PartialView("_EventSearchPartial", events);
        }
    }
}
