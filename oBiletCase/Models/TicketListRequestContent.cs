namespace oBiletCase.Models
{
    public class TicketListRequestContent
    {
        public string currentBusLocation { get; set; }
        public string destinationLocation { get; set; }
        public DateTime date { get; set; }
    }
}