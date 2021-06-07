using Layihe.DataAccesLayer;
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
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Detail(int? id)
        {
            if (id == null)
                return NotFound();

            var eventDetail = _dbContext.EventDetails.Where(x => x.IsDelete == false).Include(x => x.Event).Include(y => y.EventSpikers)
                                        .FirstOrDefault(z => z.EventId == id);

            if (eventDetail == null)
                return NotFound();

            return View(eventDetail);
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
