using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Layihe.Models
{
    public class Video
    {
        public int Id { get; set; }

        [Required]
        public string VideoTitle { get; set; }

        [Required]
        public string VideoLink { get; set; }

        [Required]
        public string TitleBoard { get; set; }
    }
}
