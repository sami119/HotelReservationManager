using HotelReservationManager.Areas.Identity.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelReservationManager.Models
{
    public class Reservation
    {
        public int ID { get; set; }

        public Room ReservedRoom { get; set; }

        public HotelManagerUser UserMadeTheReservation { get; set; }

        public List<Client> Clients { get; set; }

        public DateTime DateOfCheckIn { get; set; }

        public DateTime DateOfCheckOut { get; set; }

        public bool IncludeBreakfast { get; set; }

        public bool AllInclusive { get; set; }

        public int Cost { get; set; }
    }
}
