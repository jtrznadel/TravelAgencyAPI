using TravelAgencyAPI.Models;

namespace TravelAgencyAPI.Interfaces
{
    public interface IAccountService
    {
        public void RegisterUser(RegisterUserDto dto);
        public string GenerateJwt(LoginDto dto);
    }
}
