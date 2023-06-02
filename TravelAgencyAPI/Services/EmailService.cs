﻿using System.Net;
using System.Net.Mail;
using TravelAgencyAPI.Interfaces;
using TravelAgencyAPI.Models;

namespace TravelAgencyAPI.Services
{
    public class EmailService : IEmailService
    {
        public Task SendEmailAsync(string email, string subject, string message)
        {
            var mail = "travelagency.reservations@outlook.com";
            var pw = "Travelagency";
            var client = new SmtpClient("smtp-mail.outlook.com", 587)
            {
                EnableSsl = true,
                Credentials = new NetworkCredential(mail, pw)
            };

            return client.SendMailAsync(
                new MailMessage(from: mail,
                to: email,
                subject,
                message
                ));
        }

        public EmailModel ReservationBookedMessage(string userEmail, TourDto tour, int reservationId, bool discount)
        {
            var email = new EmailModel();
            var tourPrice = tour.Price;
            var addMessage = " ";
            if (discount) {
                tourPrice = tourPrice * 0.9;
                addMessage = "We are delighted to have you as our valued customer, and as a token of our appreciation, we are pleased to offer you a special discount of 10% on the tour price.";
            }
            email.Email = userEmail;
            email.Subject = "Reservation Confirmation for Your Tour";
            email.Message = $@"Dear {userEmail},

                    Thank you for making a reservation for the tour. We are pleased to inform you that your reservation has been successfully registered.

                    Reservation Details:
                    - Reservation Number: {reservationId}
                    - Tour: {tour.Name}
                    - Start Date: {tour.StartDate}
                    - End Date: {tour.EndDate}

                    Additional Information:
                    {addMessage}
                    - Price: {tourPrice}

                    If you have any questions or require further assistance, please don't hesitate to contact our customer service office. We are available to help you.

                    We wish you a fantastic journey and unforgettable experiences!

                    Best regards,
                    The Travel Agency Team";
            return email;
        }

        public EmailModel ReservationCanceledMessage(string userEmail, TourDto tour, int reservationId, string reason)
        {
            var email = new EmailModel();
            email.Email = userEmail;
            email.Subject = "Reservation Cancellation Confirmation";
            email.Message = $@"Dear {userEmail},

                    We regret to inform you that your reservation for the tour has been cancelled as per your request. The cancellation has been processed successfully.

                    Reservation Details:
                    - Reservation Number: {reservationId}
                    - Tour: {tour.Name}
                    - Start Date: {tour.StartDate}
                    - End Date: {tour.EndDate}

                    Additional Information:
                    - Cancellation Reason: {reason}

                    If you have any further questions or need assistance, please feel free to contact our customer service office. We are available to help you.

                    We hope to have the opportunity to serve you in the future.

                    Best regards,
                    The Travel Agency Team";
            return email;
        }

        public EmailModel ReservationFailureMessage(string userEmail, TourDto tour)
        {
            var email = new EmailModel();
            email.Email = userEmail;
            email.Subject = "Tour Fully Booked";
            email.Message = $@"Dear {userEmail},

                    We regret to inform you that the tour you have requested is fully booked. Unfortunately, all available spots have been filled.

                    Reservation Details:
                    - Tour: {tour.Name}
                    - Start Date: {tour.StartDate}
                    - End Date: {tour.EndDate}

                    We apologize for any inconvenience caused. If you have any questions or would like assistance in choosing an alternative tour, please don't hesitate to contact our customer service office. We are here to help you find another exciting adventure.

                    Thank you for considering our services, and we hope to have the opportunity to serve you in the future.

                    Best regards,
                    The Travel Agency Team";
            return email;
        }
    }
}
