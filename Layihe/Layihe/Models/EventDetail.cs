using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Layihe.Models
{
    public class EventDetail
    {
        public int Id { get; set; }
        [Required]
        public string Decscription { get; set; }
        public bool IsDelete { get; set; }

        [ForeignKey("Event")]
        public int EventId { get; set; }
        public Event Event { get; set; }
    }
}
