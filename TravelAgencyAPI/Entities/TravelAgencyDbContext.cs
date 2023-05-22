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
        public DbSet<Tour> Tours { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Tour>()
                .Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(50);
            new DbInitializer(modelBuilder).Seed();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_connectionString);
        }
    }
}
