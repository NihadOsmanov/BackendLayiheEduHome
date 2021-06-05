using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Layihe.Models
{
    public class NoticeBoard
    {
        public int Id { get; set; }

        

        [Required, DataType(DataType.DateTime)]
        public DateTime Date { get; set; }

        [Required]
        public string Descriptioon { get; set; }
        
    }
}
