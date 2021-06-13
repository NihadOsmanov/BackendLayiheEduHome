using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Layihe.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<EventCategory> EventCategories { get; set; }
        public ICollection<CourseCategory> CourseCategories { get; set; }


    }
}
