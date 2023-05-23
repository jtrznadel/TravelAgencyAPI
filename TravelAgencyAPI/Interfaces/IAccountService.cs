using TravelAgencyAPI.Entities;
using TravelAgencyAPI.Models;

namespace TravelAgencyAPI.Interfaces
{
    public interface IAccountService
    {
        public void RegisterUser(RegisterUserDto dto);
        public string GenerateJwt(LoginDto dto);
        public bool DeleteUser(int userId);
        public bool UpdateUserRole(int userId, int roleId);
        public IEnumerable<UserDto> GetAll();
    }
}
