using System.ComponentModel.DataAnnotations;

namespace TravelAgencyAPI.Models
{
    public class UpdateTourDto
    {
         public int Id { get; set; }
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
        public string Description { get; set; }
        [Required]
        public double Price { get; set; }
        [Required]
        [Range(1, 100)]
        public int TourLimit { get; set; }
    }
}
