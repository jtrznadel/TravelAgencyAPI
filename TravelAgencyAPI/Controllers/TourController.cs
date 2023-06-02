using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TravelAgencyAPI.Entities;
using TravelAgencyAPI.Interfaces;
using TravelAgencyAPI.Models;

namespace TravelAgencyAPI.Controllers
{
    [Route("tour")]
    [ApiController]
    [Authorize]
    public class TourController : ControllerBase
    {
        private readonly ITourService _tourService;
        public TourController(ITourService tourService)
        {
            _tourService = tourService;
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult<IEnumerable<Tour>> GetAll([FromQuery]TourQuery query)
        {
            var toursDtos = _tourService.GetAll(query);
            return Ok(toursDtos);
        }

        [HttpGet("manager")]
        [Authorize(Roles = "Manager")]
        public ActionResult<IEnumerable<Tour>> GetAllByOwner([FromQuery] TourQuery query)
        {
            var toursDtos = _tourService.GetAllByOwner(query);
            return Ok(toursDtos);
        }

        [HttpGet("{id}")] 
        public ActionResult Get([FromRoute]int id) 
        { 
           var tourDto = _tourService.GetById(id);
            return Ok(tourDto);
        }

        [HttpPost]
        [Authorize(Roles = "Manager")]
        public ActionResult CreateTour([FromBody]TourDto dto)
        {
            var id =  _tourService.CreateTour(dto);
            return Created($"/tour/{id}", null);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Manager, Admin")]
        public ActionResult Delete([FromRoute]int id)
        {
            var isDeleted = _tourService.DeleteById(id);
            if(isDeleted) return NoContent();
            return NotFound();
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Manager, Admin")]
        public ActionResult Update([FromBody]UpdateTourDto dto, [FromRoute]int id) 
        {
            var isUpdated = _tourService.Update(dto, id);
            if(!isUpdated) return NotFound();
            return Ok();
        }
    }
}
