namespace FlightBooking.Dtos.FlightSearchDtos
{
    public class FlightSearchResultDto
    {
        public bool Success { get; set; }

        public string? Error { get; set; }

        public string FromIata { get; set; } = "";

        public string FromAirport { get; set; } = "";

        public string ToIata { get; set; } = "";

        public string ToAirport { get; set; } = "";

        public string Currency { get; set; } = "AZN";

        public List<FlightCardDto> Flights { get; set; } = new();
    }
}