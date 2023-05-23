using TravelAgencyAPI.Entities;

namespace TravelAgencyAPI.Models
{
    public class UserDto
    {
        public int Id { get; set; }

        public string Email { get; set; }
        public string? PhoneNumber { get; set; }
        public int RoleId { get; set; }
        public virtual Role Role { get; set; }
    }
}
