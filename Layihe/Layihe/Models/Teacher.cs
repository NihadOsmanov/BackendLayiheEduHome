using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Layihe.Models
{
    public class Teacher
    {
        public int Id { get; set; }

        [Required, MaxLength(100)]
        public string FullName { get; set; }
        public bool IsDeleted { get; set; }
        public string Image { get; set; }

        [NotMapped]
        public IFormFile Photo { get; set; }
        public ICollection<SocialMediaOfTeacher> SocialMediaOfTeachers { get; set; }
        public int ProfessionOfTeacherId { get; set; }
        public ProfessionOfTeacher ProfessionOfTeacher { get; set; }
        public TeacherDetail TeacherDetail { get; set; }

    }
}
