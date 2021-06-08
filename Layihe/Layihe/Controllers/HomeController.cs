using Layihe.DataAccesLayer;
using Layihe.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Layihe.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _dbContext;

        public HomeController(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IActionResult Index()
        {
            var slider = _dbContext.Sliders.ToList();

            var homeViewModel = new HomeViewModel
            {
                Slider = slider
            };

            return View(homeViewModel);
        }
        public IActionResult Search(string search)
        {
            if (search == null)
                return NotFound();

            var searchViewModel = new SearchViewModel
            {
                Teachers = _dbContext.Teachers.Where(x => x.IsDeleted == false && x.FullName.Contains(search)).Take(4).ToList(),
                Events = _dbContext.Events.Where(x => x.IsDeleted == false && x.Name.Contains(search)).Take(4).ToList(),
                Courses = _dbContext.Courses.Where(x => x.IsDeleted == false && x.Name.Contains(search)).Take(4).ToList(),
            };
            return PartialView("_SearchPartial", searchViewModel);
        }
    }
}
