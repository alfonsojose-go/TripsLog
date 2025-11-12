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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Trip -> Accommodation
            modelBuilder.Entity<Trip>()
                .HasOne(t => t.Accommodation)
                .WithOne(a => a.Trip)
                .HasForeignKey<Accommodation>(a => a.TripId)
                .OnDelete(DeleteBehavior.Cascade);

            // Trip -> Todos
            modelBuilder.Entity<Todo>()
                .HasOne(t => t.Trip)
                .WithMany(t => t.Todos)
                .HasForeignKey(t => t.TripId)
                .OnDelete(DeleteBehavior.Cascade);


        }
    }
}
