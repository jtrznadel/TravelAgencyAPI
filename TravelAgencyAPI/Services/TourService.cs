using AutoMapper;
using TravelAgencyAPI.Entities;
using TravelAgencyAPI.Interfaces;
using TravelAgencyAPI.Models;

namespace TravelAgencyAPI.Services
{
    public class TourService : ITourService
    {
        private readonly TravelAgencyDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;
        public TourService(TravelAgencyDbContext dbContext, IMapper mapper, ILogger<TourService> logger)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _logger = logger;
        }

        public TourDto GetById(int id)
        {
            var tour = _dbContext
                .Tours
                .FirstOrDefault(tour => tour.Id == id);
            if (tour is null) return null;
            var result = _mapper.Map<TourDto>(tour);
            return result;
        }

        public IEnumerable<TourDto> GetAll()
        {
            var tours = _dbContext
                .Tours
                .ToList();
            var toursDtos = _mapper.Map<List<TourDto>>(tours);
            return toursDtos;
        }

        public int CreateTour(TourDto dto)
        {
            var tour = new Tour()
            {
                Id = dto.Id,
                Name = dto.Name,
                Description = dto.Description,
                Country = dto.Country,
                DestinationPoint = dto.DestinationPoint,
                StartDate = DateTime.ParseExact(dto.StartDate, "yyyy-MM-dd'T'HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture),
                EndDate = DateTime.ParseExact(dto.EndDate, "yyyy-MM-dd'T'HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture),
                Price = dto.Price,
                TourLimit = dto.TourLimit
            };
            _dbContext.Tours.Add(tour);
            _dbContext.SaveChanges();
            return tour.Id;
        }

        public bool DeleteById(int id)
        {
            var tour = _dbContext
                .Tours
                .FirstOrDefault(tour => tour.Id == id);
            if (tour is null) return false;
            _dbContext.Tours.Remove(tour);
            _dbContext.SaveChanges();
            return true;
        }

        public bool Update(UpdateTourDto dto, int id)
        {
            var tour = _dbContext
                .Tours
                .FirstOrDefault(tour => tour.Id == id);
            if (tour is null) return false;
            tour.Name = dto.Name;
            tour.Description = dto.Description;
            tour.Price = dto.Price;
            tour.TourLimit = dto.TourLimit;
            _dbContext.SaveChanges();
            return true;
        }
    }
}
