using System.Security.Claims;
using TravelAgencyAPI.Models;

namespace TravelAgencyAPI.Interfaces
{
    public interface ITourService
    {
        public TourDto GetById(int id);
        public PagedResult<TourDto> GetAll(TourQuery query);
        public PagedResult<TourDto> GetAllByOwner(TourQuery query);
        public int CreateTour(TourDto dto);
        public bool DeleteById(int id);
        public bool Update(UpdateTourDto dto, int id);

    }
}
