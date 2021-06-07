using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Layihe.Models
{
    public class EventSpiker
    {
        public int Id { get; set; }
        [Required, MaxLength(50)]
        public string FullName { get; set; }
        [Required]
        public string Profession { get; set; }
        [Required]
        public string Image { get; set; }
        [NotMapped]
        public IFormFile Photo { get; set; }
        public int EventDetailId { get; set; }
        public EventDetail EventDetail { get; set; }
        public bool IsDeleted { get; set; }
    }
}
