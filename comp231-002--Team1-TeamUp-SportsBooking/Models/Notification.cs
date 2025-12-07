using System;

namespace comp231_002__Team1_TeamUp_SportsBooking.Models
{
    public class Notification
    {
        public int NotificationId { get; set; }      // matches DB
        public string UserId { get; set; } = string.Empty;
        public string Message { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
        public bool IsRead { get; set; }
        public int? BookingID { get; set; }          // nullable like SQL
    }
}
