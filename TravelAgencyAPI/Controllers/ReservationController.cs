﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TravelAgencyAPI.Interfaces;
using TravelAgencyAPI.Models;
using TravelAgencyAPI.Services;

namespace TravelAgencyAPI.Controllers
{
    [Route("reservation")]
    [ApiController]
    [Authorize]
    public class ReservationController : ControllerBase
    {
        private readonly IReservationService _reservationService;
        public ReservationController(IReservationService reservationService)
        {
            _reservationService = reservationService;
        }

        [HttpPost]
        [Authorize(Roles = "User")]
        public ActionResult Create([FromBody] MakeReservationDto dto)
        {
            var id = _reservationService.Create(dto);
            return Created($"/reservation/{id}", null);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "User")]
        public ActionResult Cancel([FromRoute]int id, [FromBody]ReasonModel reason)
        {
            var isCanceled = _reservationService.Cancel(id, reason);
            return Ok();
        }

        [HttpGet]
        [Authorize(Roles = "User")]
        public ActionResult GetUserReservation()
        {
            var reservationDtos = _reservationService.GetAll();
            return Ok(reservationDtos);
        }
    }
}
