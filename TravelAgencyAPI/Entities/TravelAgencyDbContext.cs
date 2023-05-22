using Microsoft.EntityFrameworkCore;

namespace TravelAgencyAPI.Entities
{
    public class TravelAgencyDbContext : DbContext
    {
        private string _connectionString =
            "Server=(localdb)\\mssqllocaldb;Database=TravelAgencyDb;Trusted_Connection=True;";
        public TravelAgencyDbContext(DbContextOptions options) : base(options)
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
            optionsBuilder.UseSqlServer(_connectionString);
        }
    }
}
