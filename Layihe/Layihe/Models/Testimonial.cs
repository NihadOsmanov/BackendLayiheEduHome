using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Layihe.Models
{
    public class Testimonial
    {
        public int Id { get; set; }

        [Required, MaxLength(50)]
        public string PersonName { get; set; }
        [Required, MaxLength(50)]
        public string Profession { get; set; }
        [Required, MaxLength(200)]
        public string PersonAbout { get; set; }
        [Required]
        public string Image { get; set; }
        [NotMapped]
        public IFormFile Photo { get; set; }
    }
}
