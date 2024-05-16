using Newtonsoft.Json;

namespace oBiletCaseBackEnd.Models
{
    public class TicketListRequest
    {
        [JsonProperty(PropertyName = "device-session")]
        public DeviceSession DeviceSession { get; set; }
        [JsonProperty(PropertyName = "date")]
        public DateTime Date { get; set; }
        [JsonProperty(PropertyName = "language")]
        public string Language { get; set; }
        [JsonProperty(PropertyName = "data")]
        public TicketListRequestData Data { get; set; }
    }
}
