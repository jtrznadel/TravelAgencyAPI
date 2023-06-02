using System.Security.Claims;

namespace TravelAgencyAPI.Interfaces
{
    public interface IUserContextService
    {
        ClaimsPrincipal User { get; }
        int? GetUserId {  get; }
        string? GetUserRole {  get; }
    }
}
