using Microsoft.EntityFrameworkCore;
using TravelAgencyAPI.Entities;

namespace TravelAgencyAPI
{
    public class DbInitializer
    {
        private readonly TravelAgencyDbContext _dbContext;
        private readonly ModelBuilder _modelBuilder;
        public DbInitializer(ModelBuilder modelBuilder)
        {
            _modelBuilder = modelBuilder;
        }

       public void Seed()
        {
            _modelBuilder.Entity<Tour>().HasData(
                new Tour()
                {
                    Id = 1,
                    Name = "Spotlight on Sicily",
                    Description = "History and food combine on this Handcrafted seven-night tour. " +
                   "You’ll visit mountain-top towns, taste everything from pastries to cannoli pasta, " +
                   "and stand among some of the most significant Greek ruins outside of Greece.",
                    Country = "Italy",
                    DestinationPoint = "Sicily",
                    StartDate = DateTime.Parse("May 21, 2023"),
                    EndDate = DateTime.Parse("May 29, 2023"),
                    Price = 1200.00,
                    TourLimit = 20
                },
               new Tour()
               {
                   Id = 2,
                   Name = "Puglia - Italy's undiscovered heel",
                   Description = "This seven-night tour showcases some of Puglia’s prettiest sites, from vineyard-side villages to lofty cathedrals. " +
                   "Whitewashed Alberobello is home for your first five nights, then it’s off to Lecce.",
                   Country = "Italy",
                   DestinationPoint = "Puglia",
                   StartDate = DateTime.Parse("Apr 11, 2023"),
                   EndDate = DateTime.Parse("Apr 18, 2023"),
                   Price = 1120.00,
                   TourLimit = 15
               });
            _modelBuilder.Entity<Role>().HasData(
                new Role
                {
                    Id = 1,
                    Name = "User"
                },
                new Role
                {
                    Id = 2,
                    Name = "Manager"
                },
                new Role
                {
                    Id = 3,
                    Name = "Admin"
                });
        }   
    }
}
