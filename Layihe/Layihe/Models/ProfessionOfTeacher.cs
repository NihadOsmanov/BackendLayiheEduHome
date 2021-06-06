using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Layihe.Models
{
    public class ProfessionOfTeacher
    {
        public int Id { get; set; }

        [Required]
        public string Profession { get; set; }
        public ICollection<Teacher> Teachers { get; set; }
        public bool IsDeleted { get; set; }
    }
}
