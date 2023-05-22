using TravelAgencyAPI.Models;

namespace TravelAgencyAPI.Interfaces
{
    public interface ITourService
    {
        public TourDto GetById(int id);
        public IEnumerable<TourDto> GetAll();
        public int CreateTour(TourDto dto);
        public bool DeleteById(int id);
        public bool Update(UpdateTourDto dto, int id);
    }
}
