using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Layihe.Models
{
    public class Bio
    {
        public int Id { get; set; }

        [Required]
        public string FirstLogo { get; set; }
        public string SecondLogo { get; set; }
        public string FacebookUrl { get; set; }
        public string PinterestUrl { get; set; }
        public string VimeoUrl { get; set; }
        public string TwitterUrl { get; set; }
    }
}
