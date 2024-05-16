using Newtonsoft.Json;

namespace oBiletCase.Models
{
    public class Data
    {
        public int? id { get; set; }

        [JsonProperty("parent-id")]
        public int? parentid { get; set; }
        public string? type { get; set; }
        public string? name { get; set; }

        [JsonProperty("geo-location")]
        public GeoLocation? geolocation { get; set; }
        public int? zoom { get; set; }

        [JsonProperty("tz-code")]
        public string? tzcode { get; set; }

        [JsonProperty("weather-code")]
        public object? weathercode { get; set; }
        public int? rank { get; set; }

        [JsonProperty("reference-code")]
        public string? referencecode { get; set; }

        [JsonProperty("city-id")]
        public int? cityid { get; set; }

        [JsonProperty("reference-country")]
        public object? referencecountry { get; set; }

        [JsonProperty("country-id")]
        public int? countryid { get; set; }
        public string? keywords { get; set; }

        [JsonProperty("city-name")]
        public string? cityname { get; set; }
        public object? languages { get; set; }

        [JsonProperty("country-name")]
        public string? countryname { get; set; }
        public object? code { get; set; }

        [JsonProperty("show-country")]
        public bool? showcountry { get; set; }

        [JsonProperty("area-code")]
        public object? areacode { get; set; }

        [JsonProperty("long-name")]
        public string? longname { get; set; }

        [JsonProperty("is-city-center")]
        public bool? iscitycenter { get; set; }
    }

    public class GeoLocation
    {
        public double? latitude { get; set; }
        public double? longitude { get; set; }
        public int? zoom { get; set; }
    }

    public class Root
    {
        public string? status { get; set; }
        public List<Data>? data { get; set; }
    }
}
