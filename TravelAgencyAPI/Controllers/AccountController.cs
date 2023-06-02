using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TravelAgencyAPI.Entities;
using TravelAgencyAPI.Interfaces;
using TravelAgencyAPI.Models;
using TravelAgencyAPI.Services;

namespace TravelAgencyAPI.Controllers
{
    [Route("account")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public ActionResult<IEnumerable<User>> GetAll()
        {
            var userDtos = _accountService.GetAll();
            return Ok(userDtos);
        }

        [HttpGet("discount")]
        public ActionResult GetDiscount()
        {
            var result = _accountService.IsDiscountAllowed();
            return Ok(result);
        }

        [HttpPost("register")]
        public ActionResult RegisterUser([FromBody]RegisterUserDto dto)
        {
            _accountService.RegisterUser(dto);
            return Ok();
        }

        [HttpPost("login")]
        public ActionResult Login([FromBody] LoginDto dto)
        {
            string token = _accountService.GenerateJwt(dto);
            return Ok(token);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public ActionResult Delete([FromRoute]int id)
        {
            var isDeleted = _accountService.DeleteUser(id);
            if (isDeleted) return NoContent();
            return NotFound();
        }
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public ActionResult UpdateUserRole([FromRoute]int id, [FromQuery]int roleId)
        {
            var isUpdated = _accountService.UpdateUserRole(id, roleId);
            if (!isUpdated) return NotFound();
            return Ok();
        }


    }

}
