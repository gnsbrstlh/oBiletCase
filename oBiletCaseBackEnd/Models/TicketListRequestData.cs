
using Newtonsoft.Json;

namespace oBiletCaseBackEnd.Models
{
    public class TicketListRequestData
    {
        [JsonProperty(PropertyName = "origin-id")]
        public int OriginId { get; set; }

        [JsonProperty(PropertyName = "destination-id")]
        public int DestinationId { get; set; }

        [JsonProperty(PropertyName = "departure-date")]
        public DateTime DepartureDate { get; set; }
    }
}
