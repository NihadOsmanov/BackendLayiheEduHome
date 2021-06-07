using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Layihe.Models
{
    public class TeacherDetail
    {
        public int Id { get; set; }

        [Required]
        public string AboutMe { get; set; }
        public string Degree { get; set; }
        public string Experience { get; set; }
        public string Hobbies { get; set; }

        [Required]
        public string Faculty { get; set; }
        public string MailMe { get; set; }

        [Required]
        public string Number { get; set; }
        public string Skype { get; set; }
        public bool IsDeleted { get; set; }

        [ForeignKey("TeacherId")]
        public int TeacherId { get; set; }
        public Teacher Teacher { get; set; }
        public int LanguageValue { get; set; }
        public int TeamLeaderValue { get; set; }
        public int DevelopmentValue { get; set; }
        public int DesignValue { get; set; }
        public int InnovtionValue { get; set; }
        public int ComunicationValue { get; set; }
    }
}
