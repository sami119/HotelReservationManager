using Microsoft.AspNetCore.Authorization;

namespace HotelReservationManager
{
    public class IsActiveRequirment : IAuthorizationRequirement
    {
        public bool IsActive { get; }

        public IsActiveRequirment(bool isActive)
        {
            this.IsActive = isActive;
        }
    }
}