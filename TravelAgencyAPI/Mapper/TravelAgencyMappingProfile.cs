using AutoMapper;
using TravelAgencyAPI.Entities;
using TravelAgencyAPI.Models;

namespace TravelAgencyAPI.Mapper
{
    public class TravelAgencyMappingProfile : Profile
    {
        public TravelAgencyMappingProfile()
        {
            CreateMap<Tour, TourDto>();
            CreateMap<TourDto, Tour>();
            CreateMap<User, UserDto>();
            CreateMap<MakeReservationDto, Reservation>();
        }
    }
}
