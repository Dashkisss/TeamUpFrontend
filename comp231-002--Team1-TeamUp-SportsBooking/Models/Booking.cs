namespace comp231_002__Team1_TeamUp_SportsBooking.Models
{
    public class Booking
    {
        public int BookingID { get; set; }

        public int PlayerID { get; set; }
        public Player? Player { get; set; }

        public int CourtID { get; set; }
        public Court? Court { get; set; }

        public DateTime BookingDate { get; set; }
        public string? TimeSlot { get; set; }
    }
}
