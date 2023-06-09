﻿using TravelAgencyAPI.Models;

namespace TravelAgencyAPI.Interfaces
{
    public interface IEmailService
    {
        Task SendEmailAsync(string email, string subject, string body);
        public EmailModel ReservationBookedMessage(string userEmail, TourDto tour, int reservationId, bool discount);
        public EmailModel ReservationCanceledMessage(string userEmail, TourDto tour, int reservationId, string reason);
        public EmailModel ReservationFailureMessage(string userEmail, TourDto tour);
    }
}
