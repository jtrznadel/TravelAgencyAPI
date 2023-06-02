using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TravelAgencyAPI.Entities;
using TravelAgencyAPI.Exceptions;
using TravelAgencyAPI.Interfaces;
using TravelAgencyAPI.Middleware;
using TravelAgencyAPI.Models;

namespace TravelAgencyAPI.Services
{
    public class AccountService : IAccountService
    {
        private readonly TravelAgencyDbContext _dbContext;
        private readonly IPasswordHasher<User> _passwordHasher;
        private readonly AuthenticationSettings _authenticationSettings;
        private readonly IMapper _mapper;

        public AccountService(TravelAgencyDbContext dbContext, IPasswordHasher<User> passwordHasher, AuthenticationSettings authenticationSettings, IMapper mapper)
        {
            _dbContext = dbContext;
            _passwordHasher = passwordHasher;
            _authenticationSettings = authenticationSettings;
            _mapper = mapper;
        }

        public string GenerateJwt(LoginDto dto)
        {
            var user = _dbContext.Users
                .Include(u => u.Role)
                .FirstOrDefault(u => u.Email == dto.Email);
            if(user is null) throw new BadRequestException("Email or password is incorrect");
            var result = _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, dto.Password);
            if (result == PasswordVerificationResult.Failed)
            {
                throw new BadRequestException("Email or password is incorrect");
            }

            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, $"{user.FirstName} {user.LastName}"),
                new Claim(ClaimTypes.Role, $"{user.Role.Name}"),
            };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_authenticationSettings.JwtKey));
            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.Now.AddDays(_authenticationSettings.JwtExpireDays);
            var token = new JwtSecurityToken(_authenticationSettings.JwtIssuer,
                _authenticationSettings.JwtIssuer,
                claims,
                expires: expires,
                signingCredentials: cred);
            var tokenHandler = new JwtSecurityTokenHandler();
            return tokenHandler.WriteToken(token);
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

        public bool DeleteUser(int userId)
        {
            var userToDelete = _dbContext
                .Users
                .FirstOrDefault(u => u.Id == userId);

            if (userToDelete is null) return false;

            _dbContext.Users.Remove(userToDelete);
            _dbContext.SaveChanges();
            return true;
        }

        public bool UpdateUserRole(int userId, int roleId)
        {
            var userToUpdate = _dbContext
                .Users
                .FirstOrDefault(u => u.Id == userId);
            if (userToUpdate is null) return false;
            userToUpdate.RoleId = roleId;
            _dbContext.SaveChanges();
            return true;
        }

        public IEnumerable<UserDto> GetAll()
        {
            var users = _dbContext
                .Users
                .ToList();
            var usersDtos = _mapper.Map<List<UserDto>>(users);
            return usersDtos;
        }

        public bool IsDiscountAllowed(int userId)
        {
            var date6MonthsBack = DateTime.Now.AddMonths(-6);
            var userRecentReservations = _dbContext.Reservations.Where(r => r.UserId ==  userId).ToList();
            var counter = userRecentReservations.Where(r => r.ReservatedAt > date6MonthsBack && r.Status == "Ongoing").Count();
            if (counter > 3) return true;
            return false;
        }
    }
}
