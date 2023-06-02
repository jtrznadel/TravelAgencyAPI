using Microsoft.EntityFrameworkCore;

namespace TravelAgencyAPI.Entities
{
    public class TravelAgencyDbContext : DbContext
    {
        public TravelAgencyDbContext(DbContextOptions<TravelAgencyDbContext> options) : base(options)
        {
            
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Tour> Tours { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .Property(t => t.Email)
                .IsRequired();
            modelBuilder.Entity<Role>()
               .Property(t => t.Name)
               .IsRequired();         
            new DbInitializer(modelBuilder).Seed();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
           
        }
    }
}
