using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using TravelAgencyAPI.Entities;

namespace TravelAgencyAPI.Authorization
{
    public class ReservationRequirementHandler : AuthorizationHandler<ResourceOperationRequirement, Reservation>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, ResourceOperationRequirement requirement, Reservation resource)
        {
            if (requirement.ResourceOperation == ResourceOperation.Read ||
                 requirement.ResourceOperation == ResourceOperation.Create)
            {
                context.Succeed(requirement);
            }
            var userId = context.User.FindFirst(c => c.Type == ClaimTypes.NameIdentifier).Value;
            if (resource.UserId == int.Parse(userId))
            {
                context.Succeed(requirement);
            }
            return Task.CompletedTask;
        }
    }
}
