using Layihe.DataAccesLayer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Layihe.ViewComponents
{
    public class TestimonialViewComponent : ViewComponent
    {
        private readonly AppDbContext _dbContext;

        public TestimonialViewComponent(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var testimonial = await _dbContext.Testimonials.ToListAsync();

            return View(testimonial);
        }
    }
}
