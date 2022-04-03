using Microsoft.EntityFrameworkCore;
using Taallama.Domain.Entities;

namespace Taallama.Data.Contexts
{
    public class TaallamaDbContext : DbContext
    {
        public TaallamaDbContext(DbContextOptions<TaallamaDbContext> options) : base(options)
        { }

        DbSet<User> Users { get; set; }
        DbSet<Course> Courses { get; set; }
        DbSet<Video> Videos { get; set; }
    }
}