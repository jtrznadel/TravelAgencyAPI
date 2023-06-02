using TravelAgencyAPI.Entities;

namespace TravelAgencyAPI.Models
{
    public class ReservationDto
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int TourId { get; set; }
        public DateTime ReservatedAt { get; set; }
        public string Status { get; set; }
        public virtual User User { get; set; }
        public virtual Tour Tour { get; set; }
    }
}
