namespace comp231_002__Team1_TeamUp_SportsBooking.Models
{
    public class Game
    {
        public int GameID { get; set; }
        public int TeamId { get; set; }
        public int CourtID { get; set; }
        public DateTime GameDate { get; set; }
        public string Status { get; set; }  


    }
}
