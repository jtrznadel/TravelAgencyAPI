using TravelAgencyAPI.Models;

namespace TravelAgencyAPI.Interfaces
{
    public interface IReservationService
    {
        public int Create(MakeReservationDto dto);
        public bool Cancel(int  reservationId);
    }
}
