namespace comp231_002__Team1_TeamUp_SportsBooking.Models
{
    
        public class Player
        {
            public int PlayerID { get; set; }
            public string? Name { get; set; }
            public string? Email { get; set; }
            public string? SportPreference { get; set; }

            public ICollection<Booking>? Bookings { get; set; }
        }
    }

