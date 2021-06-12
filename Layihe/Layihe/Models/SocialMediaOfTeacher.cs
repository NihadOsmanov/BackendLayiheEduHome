using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Layihe.Models
{
    public class SocialMediaOfTeacher
    {
        public int Id { get; set; }

        [Required]
        public string Icon { get; set; }

        [Required]
        public string Link { get; set; }
        public int TeacherId { get; set; }
        public Teacher Teacher { get; set; }
        public bool IsDeleted { get; set; }
    }
}
