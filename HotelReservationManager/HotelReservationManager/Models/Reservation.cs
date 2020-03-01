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

        public string ReservedRoomID { get; set; }

        public string UserName { get; set; }

        public string ClientName { get; set; }

        public DateTime DateOfCheckIn { get; set; }

        public DateTime DateOfCheckOut { get; set; }

        public bool IncludeBreakfast { get; set; }

        public bool AllInclusive { get; set; }

        public decimal Cost { get; set; }
    }
}
