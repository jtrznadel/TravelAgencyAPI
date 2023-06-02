using TravelAgencyAPI.Entities;
using TravelAgencyAPI.Models;

namespace TravelAgencyAPI.Interfaces
{
    public interface IReservationService
    {
        public Task<Reservation> Create(MakeReservationDto dto);
        public Task<Reservation> Cancel(int  reservationId, ReasonModel reason);
        public IEnumerable<ReservationDto> GetAll();
        public int GetTourReservations (int tourId);
    }
}
