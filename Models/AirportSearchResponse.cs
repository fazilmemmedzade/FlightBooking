using System.Text.Json.Serialization;

namespace FlightBooking.Models
{
    public class AirportSearchResponse
    {
        [JsonPropertyName("status")]
        public bool Status { get; set; }

        [JsonPropertyName("message")]
        public string? Message { get; set; }

        [JsonPropertyName("message")]
        public List<AirportSearchData> Data { get; set; } = new();
    }
}
