using Layihe.DataAccesLayer;
using Layihe.Models;
using Layihe.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text.RegularExpressions;
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

        #region Search
        public IActionResult Search(string search)
        {
            if (search == null)
                return NotFound();

            var searchViewModel = new SearchViewModel
            {
                Teachers = _dbContext.Teachers.Where(x => x.IsDeleted == false && x.FullName.Contains(search)).Take(4).ToList(),
                Events = _dbContext.Events.Where(x => x.IsDeleted == false && x.Name.Contains(search)).Take(4).ToList(),
                Courses = _dbContext.Courses.Where(x => x.IsDeleted == false && x.Name.Contains(search)).Take(4).ToList(),
                Blogs = _dbContext.Blogs.Where(x => x.IsDeleted == false && x.Title.Contains(search)).Take(4).ToList(),
            };
            return PartialView("_SearchViewPartial", searchViewModel);
        }

        #endregion

        #region Subscriber
        public async Task<IActionResult> Subscriber(string email)
        {
            string pattern = "^(([a-zA-Z0-9_\\-\\.]+)@([a-zA-Z0-9_\\-\\.]+)\\.([a-zA-Z]{2,5}){1,25})+([;.](([a-zA-Z0-9_\\-\\.]+)@([a-zA-Z0-9_\\-\\.]+)\\.([a-zA-Z]{2,5}){1,25})+)*$";

            if (email == null)
            {
                return Content("Email can not empty");
            }

            if (!Regex.IsMatch(email, pattern))
            {
                return Content("Email is not valid");
            }

            var dbSubsriber = _dbContext.Subscribers.ToList();

            var subscriber = new Subscriber()
            {
                Email = email
            };

            foreach (var item in dbSubsriber)
            {
                if (item.Email == email)
                {
                    return Content("this account allready is subscriber");
                }
            }

            await _dbContext.AddAsync(subscriber);
            await _dbContext.SaveChangesAsync();

            return Content("Suceess");
        }

        #endregion
    }
}
