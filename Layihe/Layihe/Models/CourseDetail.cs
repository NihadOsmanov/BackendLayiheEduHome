using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Layihe.Models
{
    public class CourseDetail
    {
        public int Id { get; set; }
        public string AboutCourse { get; set; }
        public string HowToApply { get; set; }
        public string Certification { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public string Duration { get; set; }

        [Required]
        public string ClassDuration { get; set; }

        [Required]
        public string Language { get; set; }

        [Required]
        public int Students { get; set; }
        public string Assesments { get; set; }

        [Required]
        public int Price { get; set; }
        public bool IsDeleted { get; set; }

        [ForeignKey("Course")]
        public int CourseId { get; set; }
        public Course Course { get; set; }
    }
}
