using Microsoft.EntityFrameworkCore;

namespace TripsLog.Models
{
    public class TripsDbContext : DbContext
    {
        public TripsDbContext(DbContextOptions<TripsDbContext> options)
            : base(options)
        {
        }
        public DbSet<Trip> Trips { get; set; }
        public DbSet<Accommodation> Accommodations { get; set; }
        public DbSet<Todo> Todos { get; set; }
    }
}
