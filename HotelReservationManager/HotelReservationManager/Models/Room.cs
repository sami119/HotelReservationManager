namespace HotelReservationManager.Models
{
    public class Room
    {
        public int ID { get; set; }

        public int Capacity { get; set; }

        public Type _Type { get; set; }

        public bool IsAvailable { get; set; }

        public decimal PricePerBedForAdult { get; set; }

        public decimal PricePerBedForChild { get; set; }

        public int RoomNumber { get; set; }

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
