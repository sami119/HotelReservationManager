using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelReservationManager.Services
{
    interface IUserIdentityProvider
    {
        string GetCurrentUserId();
    }
}
