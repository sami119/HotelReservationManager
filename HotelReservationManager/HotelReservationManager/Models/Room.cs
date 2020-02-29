using System.ComponentModel.DataAnnotations;

namespace HotelReservationManager.Models
{
    public class Room
    {

        public int ID
        {
            get; set;
        }

        [Required]
        [Range(0, 10)]
        public int Capacity
        {
            get; set;
        }

        public Type _Type
        {
            get; set;
        }

        public bool IsAvailable
        {
            get; set;
        }

        [Required]
        [Range(0.0, 999.99)]
        public decimal PricePerBedForAdult
        {
            get; set;
        }

        [Required]
        [Range(0.0, 999.99)]
        public decimal PricePerBedForChild
        {
            get; set;
        }

        [Required]
        public string RoomNumber
        {
            get; set;
        }

        public enum Type
        {
            ТwoSingleBeds,
            Apartament,
            RoomWithDoubleBed,
            Penthouse,
            Мaisonette
        }
    }
}
