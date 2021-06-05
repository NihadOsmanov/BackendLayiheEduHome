using Layihe.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Layihe.DataAccesLayer
{
    public class AppDbContext : DbContext
    {
        private readonly AppDbContext _dbContext;

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
    }
}
