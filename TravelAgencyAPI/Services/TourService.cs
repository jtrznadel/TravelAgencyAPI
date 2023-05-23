using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using System.Linq.Expressions;
using System.Security.Claims;
using TravelAgencyAPI.Authorization;
using TravelAgencyAPI.Entities;
using TravelAgencyAPI.Exceptions;
using TravelAgencyAPI.Interfaces;
using TravelAgencyAPI.Models;

namespace TravelAgencyAPI.Services
{
    public class TourService : ITourService
    {
        private readonly TravelAgencyDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;
        private readonly IAuthorizationService _authorizationService;
        private readonly IUserContextService _userContextService;
        public TourService(TravelAgencyDbContext dbContext, IMapper mapper, ILogger<TourService> logger, 
            IAuthorizationService authorizationService, IUserContextService userContextService)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _logger = logger;
            _authorizationService = authorizationService;
            _userContextService = userContextService;
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

        public PagedResult<TourDto> GetAll(TourQuery query)
        {
            var baseQuery = _dbContext
                .Tours
                .Where(t => query.SearchPhrase == null || (t.Country.ToLower().Contains(query.SearchPhrase.ToLower())
                                                    || t.DestinationPoint.ToLower().Contains(query.SearchPhrase.ToLower())));

            if (!string.IsNullOrEmpty(query.SortBy))
            {
                var columnsSelector = new Dictionary<string, Expression<Func<Tour, object>>>
                {
                    {nameof(Tour.Name), t => t.Name },
                    {nameof(Tour.Country), t => t.Country },
                    {nameof(Tour.StartDate), t => t.StartDate }
                };

                var selectedColumn = columnsSelector[query.SortBy];

                baseQuery = query.SortDirection == SortDirection.ASC 
                    ? baseQuery.OrderBy(selectedColumn)
                    : baseQuery.OrderByDescending(selectedColumn);
            }

            var tours = baseQuery
                .Skip(query.PageSize * (query.PageNumber -1))
                .Take(query.PageSize)
                .ToList();

            var toursDtos = _mapper.Map<List<TourDto>>(tours);

            var totalItemsCount = baseQuery.Count();

            var result = new PagedResult<TourDto>(toursDtos, totalItemsCount, query.PageSize, query.PageNumber);

            return result;
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
                StartDate = dto.StartDate,
                EndDate = dto.EndDate,
                Price = dto.Price,
                TourLimit = dto.TourLimit
            };
            tour.CreatedById = _userContextService.GetUserId;
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

            var authorizationResult = _authorizationService.AuthorizeAsync(_userContextService.User, tour, new ResourceOperationRequirement(ResourceOperation.Delete)).Result;

            if (!authorizationResult.Succeeded)
            {
                throw new ForbidException();
            }

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

            var authorizationResult = _authorizationService.AuthorizeAsync(_userContextService.User, tour, new ResourceOperationRequirement(ResourceOperation.Update)).Result;

            if (!authorizationResult.Succeeded)
            {
                throw new ForbidException();
            }

            tour.Name = dto.Name;
            tour.Description = dto.Description;
            tour.Price = dto.Price;
            tour.TourLimit = dto.TourLimit;
            _dbContext.SaveChanges();
            return true;
        }
    }
}
