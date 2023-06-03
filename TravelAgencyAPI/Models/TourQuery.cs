namespace TravelAgencyAPI.Models
{
    public class TourQuery
    {
        public string? SearchPhrase { get; set; }
        public DateTime? SearchDate { get; set; }
        public int? SearchPrice { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public string? SortBy { get; set; }
        
        public SortDirection SortDirection { get; set; }
    }
}
