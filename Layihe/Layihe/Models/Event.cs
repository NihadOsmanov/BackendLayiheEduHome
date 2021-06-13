using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Layihe.Models
{
    public class Event
    {
        public int Id { get; set; }

        [Required, MaxLength(50)]
        public string Name { get; set; }

        [Required, MaxLength(150)]
        public string Adress { get; set; }

        public string Image { get; set; }
        [NotMapped]
        public IFormFile Photo { get; set; }

        public bool IsDeleted { get; set; }

        [Required]
        public DateTime StartingTime { get; set; }
        [Required]
        public DateTime EndTime { get; set; }
        public EventDetail EventDetail { get; set; }
        public ICollection<EventSpiker> EventSpikers { get; set; }
        public ICollection<EventCategory> EventCategories { get; set; }

    }
}
