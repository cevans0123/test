using Microsoft.EntityFrameworkCore;

namespace Ideas.Models
{
    public class MyContext : DbContext
    {
        public MyContext(DbContextOptions<MyContext> options) : base(options) { }
        public DbSet<User> Users { get; set; }
        public DbSet<Idea> Ideas { get; set; }
        public DbSet<Participant> Participants { get; set; }
    }
}