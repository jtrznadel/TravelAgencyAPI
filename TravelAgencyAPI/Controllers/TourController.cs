using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TravelAgencyAPI.Entities;
using TravelAgencyAPI.Interfaces;
using TravelAgencyAPI.Models;

namespace TravelAgencyAPI.Controllers
{
    [Route("tour")]
    [ApiController]
    public class TourController : ControllerBase
    {
        private readonly ITourService _tourService;
        public TourController(ITourService tourService)
        {
            _tourService = tourService;
        }

        public ActionResult<IEnumerable<Tour>> GetAll()
        {
            var toursDtos = _tourService.GetAll();
            return Ok(toursDtos);
        }

        [HttpGet("{id}")]
        public ActionResult Get([FromRoute]int id) 
        { 
           var tourDto = _tourService.GetById(id);
            return Ok(tourDto);
        }

        [HttpPost]
        public ActionResult CreateTour([FromBody]TourDto dto)
        {
            var id =  _tourService.CreateTour(dto);
            return Created($"/tour/{id}", null);
        }

        [HttpDelete("{id}")]
        public ActionResult Delete([FromRoute]int id)
        {
            var isDeleted = _tourService.DeleteById(id);
            if(isDeleted) return NoContent();
            return NotFound();
        }

        [HttpPut("{id}")]
        public ActionResult Update([FromBody]UpdateTourDto dto, [FromRoute]int id) 
        {
            var isUpdated = _tourService.Update(dto, id);
            if(!isUpdated) return NotFound();
            return Ok();
        }
    }
}
