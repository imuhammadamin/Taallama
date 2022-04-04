using Microsoft.EntityFrameworkCore;
using Taallama.Domain.Entities;

namespace Taallama.Data.Contexts
{
    public class TaallamaDbContext : DbContext
    {
        public TaallamaDbContext(DbContextOptions<TaallamaDbContext> options) : base(options)
        { }

        public DbSet<User> Users { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Video> Videos { get; set; }
    }
}