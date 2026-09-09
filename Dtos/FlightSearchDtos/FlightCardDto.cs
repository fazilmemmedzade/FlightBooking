namespace FlightBooking.Dtos.FlightSearchDtos
{
    public class FlightCardDto
    {
        public string Airline { get; set; } = "";

        public string AirlineIata { get; set; } = "";

        public string FlightNumber { get; set; } = "";

        public string DepartureTime { get; set; } = "";

        public string ArrivalTime { get; set; } = "";

        public string DepartureAirport { get; set; } = "";

        public string ArrivalAirport { get; set; } = "";

        public string DurationText { get; set; } = "";

        public string Status { get; set; } = "";
    }
}