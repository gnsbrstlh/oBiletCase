using Newtonsoft.Json;

namespace oBiletCaseBackEnd.Models
{
    public class GetBusLocationRequest
    {
        [JsonProperty(PropertyName = "data")]
        public object Data { get; set; }
        [JsonProperty(PropertyName = "device-session")]
        public DeviceSession DeviceSession { get; set; }
        [JsonProperty(PropertyName = "date")]
        public DateTime Date { get; set; }
        [JsonProperty(PropertyName = "language")]
        public string Language { get; set; }
    }
}
