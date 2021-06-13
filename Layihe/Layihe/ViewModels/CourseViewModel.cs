using Layihe.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Layihe.ViewModels
{
    public class CourseViewModel
    {
        public List<Blog> Blogs { get; set; }
        public List<Category> Categories { get; set; }
        public CourseDetail CourseDetail { get; set; }
    }
}
