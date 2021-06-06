using Layihe.DataAccesLayer;
using Microsoft.AspNetCore.Mvc;
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
