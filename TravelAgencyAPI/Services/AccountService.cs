using Microsoft.AspNetCore.Identity;
using TravelAgencyAPI.Entities;
using TravelAgencyAPI.Interfaces;
using TravelAgencyAPI.Models;

namespace TravelAgencyAPI.Services
{
    public class AccountService : IAccountService
    {
        private readonly TravelAgencyDbContext _dbContext;
        private readonly IPasswordHasher<User> _passwordHasher;

        public AccountService(TravelAgencyDbContext dbContext, IPasswordHasher<User> passwordHasher) 
        {
            _dbContext = dbContext;
            _passwordHasher = passwordHasher;
        }

        public void RegisterUser(RegisterUserDto dto)
        {
            var newUser = new User()
            {
                Email = dto.Email,
                DateOfBirth = dto.DateOfBirth,
                RoleId = dto.RoleId
            };
            newUser.PasswordHash = _passwordHasher.HashPassword(newUser, dto.Password);
            _dbContext.Users.Add(newUser);
            _dbContext.SaveChanges();   
        }
    }
}
