using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace HotelReservationManager.Areas.Identity.Data
{
    // Add profile data for application users by adding properties to the HotelManagerUser class
    public class HotelManagerUser : IdentityUser
    {
        [PersonalData]
        public string Name { get; set; }

        [PersonalData]
        public string Surname { get; set; }

        [PersonalData]
        public string Familyname { get; set; }

        [PersonalData]
        public string EGN { get; set; }

        public bool IsActive { get; set; }

        [PersonalData]
        public DateTime DateOfAppointment { get; set; }

        [PersonalData]
        public DateTime DateOfDismissal { get; set; }
    }
}
