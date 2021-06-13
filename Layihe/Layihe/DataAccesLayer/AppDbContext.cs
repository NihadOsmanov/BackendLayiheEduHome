using Layihe.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Layihe.DataAccesLayer
{
    public class AppDbContext : IdentityDbContext<User>
    {
        

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }
        public DbSet<Bio> Bios { get; set; }
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<Slider> Sliders { get; set; }
        public DbSet<AboutArea> AboutAreas { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<NoticeBoard> NoticeBoards { get; set; }
        public DbSet<Video> Videos { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<Testimonial> Testimonials { get; set; }
        public DbSet<Blog> Blogs { get; set; }
        public DbSet<BannerArea> BannerAreas { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<SocialMediaOfTeacher> SocialMediaOfTeachers { get; set; }
        public DbSet<ProfessionOfTeacher> ProfessionOfTeachers { get; set; }
        public DbSet<TeacherDetail> TeacherDetails { get; set; }
        public DbSet<CourseDetail> CourseDetails { get; set; }
        public DbSet<EventDetail> EventDetails { get; set; }
        public DbSet<Spiker> Spikers { get; set; }
        public DbSet<BlogDetail> BlogDetails { get; set; }
        public DbSet<EventSpiker> EventSpikers { get; set; }
        public DbSet<Subscriber> Subscribers { get; set; }
        public DbSet<SocialMedia> SocialMedias { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<EventCategory> EventCategories { get; set; }
        public DbSet<CourseCategory> CourseCategories { get; set; }
        public DbSet<BlogCategory> BlogCategories { get; set; }
    }
}
