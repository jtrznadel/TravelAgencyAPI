using System.ComponentModel.DataAnnotations;

namespace TravelAgencyAPI.Models
{
    public class TourDto
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
        public string Description { get; set; }
        [Required]
        [MaxLength(20)]
        public string Country { get; set; }
        [Required]
        [MaxLength(50)]
        public string DestinationPoint { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        [Required]
        public double Price { get; set; }
        [Required]
        [Range(1, 100)]
        public int TourLimit { get; set; }
    }
}
