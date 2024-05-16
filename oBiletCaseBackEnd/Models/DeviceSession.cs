using Newtonsoft.Json;

namespace oBiletCaseBackEnd.Models
{
    public class DeviceSession
    {
        [JsonProperty(PropertyName = "session-id")]
        public string SessionId { get; set; }
        [JsonProperty(PropertyName = "device-id")]
        public string DeviceId { get; set; }
    }
}
