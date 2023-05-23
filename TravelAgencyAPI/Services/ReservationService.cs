using AutoMapper;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using TravelAgencyAPI.Authorization;
using TravelAgencyAPI.Entities;
using TravelAgencyAPI.Exceptions;
using TravelAgencyAPI.Interfaces;
using TravelAgencyAPI.Models;

namespace TravelAgencyAPI.Services
{
    public class ReservationService : IReservationService
    {
        private readonly TravelAgencyDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly IUserContextService _userContextService;
        private readonly IAuthorizationService _authorizationService;
        public ReservationService(TravelAgencyDbContext dbContext, IMapper mapper, IUserContextService userContextService, IAuthorizationService authorizationService)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _userContextService = userContextService;
            _authorizationService = authorizationService;
        }
        public int Create(MakeReservationDto dto)
        {
            var reservation = _mapper.Map<Reservation>(dto);
            reservation.UserId = (int)_userContextService.GetUserId;
            reservation.ReservatedAt = DateTime.UtcNow;
            reservation.Status = "Ongoing";
            _dbContext.Add(reservation);
            _dbContext.SaveChanges();
            return reservation.Id;
        }

        public bool Cancel(int reservationId)
        {
            var reservation = _dbContext
                .Reservations
                .FirstOrDefault(r => r.Id == reservationId);
            if (reservation == null) return false;

            var authorizationResult = _authorizationService.AuthorizeAsync(_userContextService.User, reservation, new ResourceOperationRequirement(ResourceOperation.Update)).Result;
            if(!authorizationResult.Succeeded)
            {
                throw new ForbidException();
            }
            reservation.Status = "Canceled";
            _dbContext.SaveChanges();
            return true;
        }
    }
}
