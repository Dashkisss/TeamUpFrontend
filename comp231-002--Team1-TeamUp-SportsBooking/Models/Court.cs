namespace comp231_002__Team1_TeamUp_SportsBooking.Models
{
    public class Court
    {
        public int CourtID { get; set; }
        public string? Location { get; set; }
        public string? SurfaceType { get; set; }
        public bool IsAvailable { get; set; }

        public ICollection<Booking>? Bookings { get; set; }
    }
}
