using AutoMapper;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
        private readonly IEmailService _emailService;
        private readonly IAccountService _accountService;
        public ReservationService(TravelAgencyDbContext dbContext, IMapper mapper, IUserContextService userContextService, IAuthorizationService authorizationService, IEmailService emailService, IAccountService accountService)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _userContextService = userContextService;
            _authorizationService = authorizationService;
            _emailService = emailService;
            _accountService = accountService;
        }
        public async Task<Reservation> Create(MakeReservationDto dto)
        {
            var reservation = _mapper.Map<Reservation>(dto);
            reservation.UserId = (int)_userContextService.GetUserId;
            reservation.ReservatedAt = DateTime.UtcNow;
            reservation.Status = "Ongoing";
            var tourTemp = _dbContext.Tours.Where(t => t.Id == dto.TourId).FirstOrDefault();
            var placesTaken = _dbContext.Reservations.Where(r => r.TourId == dto.TourId).Count();
            bool result = tourTemp.TourLimit > placesTaken ? true : false;
            var user = _dbContext.Users.Where(u => u.Id == reservation.UserId).FirstOrDefault();
            var tour = _dbContext.Tours.Where(t => t.Id == reservation.TourId).FirstOrDefault();
            var tourDto = _mapper.Map<TourDto>(tour);
            if (!result)
            {
                var email = _emailService.ReservationFailureMessage(user.Email, tourDto);
                await _emailService.SendEmailAsync(email.Email, email.Subject, email.Message);
                return reservation;
            }
            else
            {
                _dbContext.Add(reservation);
                _dbContext.SaveChanges();

                var discount = _accountService.IsDiscountAllowed();
                var email = _emailService.ReservationBookedMessage(user.Email, tourDto, reservation.Id, discount);
                await _emailService.SendEmailAsync(email.Email, email.Subject, email.Message);
                return reservation;
            }           
        }

        public async Task<Reservation> Cancel(int reservationId, ReasonModel reason)
        {
            var reservation = _dbContext
                .Reservations
                .FirstOrDefault(r => r.Id == reservationId);
            if (reservation == null) return reservation;

            var authorizationResult = _authorizationService.AuthorizeAsync(_userContextService.User, reservation, new ResourceOperationRequirement(ResourceOperation.Update)).Result;
            if(!authorizationResult.Succeeded)
            {
                throw new ForbidException();
            }
            reservation.Status = "Canceled";
            _dbContext.SaveChanges();

            var user = _dbContext.Users.Where(u => u.Id == reservation.UserId).FirstOrDefault();
            var tour = _dbContext.Tours.Where(t => t.Id == reservation.TourId).FirstOrDefault();
            var tourDto = _mapper.Map<TourDto>(tour);
            var email = _emailService.ReservationCanceledMessage(user.Email, tourDto, reservation.Id, reason.Reason);
            await _emailService.SendEmailAsync(email.Email, email.Subject, email.Message);

            return reservation;
        }

        public IEnumerable<ReservationDto> GetAll()
        {
            var userId = (int)_userContextService.GetUserId;
            var reservations = _dbContext
                .Reservations
                .Where(r => r.UserId == userId)
                .Include(r => r.Tour)
                .ToList();
            var reservationDtos = _mapper.Map<List<ReservationDto>>(reservations);
            return reservationDtos;
        }

        public int GetTourReservations(int tourId)
        {
            return _dbContext.Reservations.Where(r => r.TourId == tourId).Count();
        }
    }
}
