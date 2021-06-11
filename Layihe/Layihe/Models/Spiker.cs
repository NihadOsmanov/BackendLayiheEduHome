using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Layihe.Models
{
    public class Spiker
    {
        public int Id { get; set; }
        [Required, MaxLength(50)]
        public string FullName { get; set; }
        [Required]
        public string Profession { get; set; }
        public string Image { get; set; }
        [NotMapped]
        public IFormFile Photo { get; set; }
        public bool IsDeleted { get; set; }
        public ICollection<EventSpiker> EventSpikers { get; set; }

    }
}
