using FlightBooking.Dtos.FlightSearchDtos;
using System.Globalization;
using System.Text.Json;

namespace FlightBooking.Services.AirportServices.FlightSearchServices
{
    public class FlightSearchService : IFlightSearchService
    {
        private readonly HttpClient _client;
        private readonly IConfiguration _configuration;

        public FlightSearchService(
            HttpClient client,
            IConfiguration configuration)
        {
            _client = client;
            _configuration = configuration;
        }

        public async Task<List<FlightCardDto>> SearchAsync(
            string fromIata,
            string toIata,
            string outboundDate,
            int adults,
            string cabin,
            string currency)
        {
            var cards = new List<FlightCardDto>();

            if (string.IsNullOrWhiteSpace(fromIata) ||
                string.IsNullOrWhiteSpace(toIata))
            {
                return cards;
            }

            var apiKey = _configuration["AviationStack:ApiKey"];
            var baseUrl = _configuration["AviationStack:BaseUrl"];

            if (string.IsNullOrWhiteSpace(apiKey))
            {
                throw new InvalidOperationException(
                    "AviationStack API açarı konfiqurasiyada tapılmadı.");
            }

            if (string.IsNullOrWhiteSpace(baseUrl))
            {
                throw new InvalidOperationException(
                    "AviationStack BaseUrl konfiqurasiyada tapılmadı.");
            }

            var url =
                $"{baseUrl.TrimEnd('/')}/flights" +
                $"?access_key={Uri.EscapeDataString(apiKey)}" +
                $"&dep_iata={Uri.EscapeDataString(fromIata.ToUpperInvariant())}" +
                $"&arr_iata={Uri.EscapeDataString(toIata.ToUpperInvariant())}";

            using var response = await _client.GetAsync(url);

            var body = await response.Content.ReadAsStringAsync();

            using var document = JsonDocument.Parse(body);

            if (document.RootElement.TryGetProperty(
                    "error",
                    out var error))
            {
                var message =
                    "Aviationstack API xətası baş verdi.";

                if (error.TryGetProperty(
                        "message",
                        out var messageElement))
                {
                    message =
                        messageElement.GetString() ?? message;
                }

                throw new InvalidOperationException(message);
            }

            if (!response.IsSuccessStatusCode)
            {
                throw new InvalidOperationException(
                    "Aviationstack sorğusu uğursuz oldu.");
            }

            if (!document.RootElement.TryGetProperty(
                    "data",
                    out var data))
            {
                return cards;
            }

            if (data.ValueKind != JsonValueKind.Array)
            {
                return cards;
            }

            foreach (var flight in data.EnumerateArray())
            {
                var airline = GetNestedString(
                    flight,
                    "airline",
                    "name");

                var airlineIata = GetNestedString(
                    flight,
                    "airline",
                    "iata");

                var flightNumber = GetNestedString(
                    flight,
                    "flight",
                    "iata");

                var departureAirport = GetNestedString(
                    flight,
                    "departure",
                    "iata");

                var arrivalAirport = GetNestedString(
                    flight,
                    "arrival",
                    "iata");

                var departureTime = GetNestedString(
                    flight,
                    "departure",
                    "scheduled");

                var arrivalTime = GetNestedString(
                    flight,
                    "arrival",
                    "scheduled");

                var status = GetString(
                    flight,
                    "flight_status");

                var durationText = CalculateDuration(
                    departureTime,
                    arrivalTime);

                if (string.IsNullOrWhiteSpace(airline))
                {
                    airline = "Unknown Airline";
                }

                cards.Add(new FlightCardDto
                {
                    Airline = airline,
                    AirlineIata = airlineIata,
                    FlightNumber = flightNumber,

                    DepartureTime =
                        ExtractClock(departureTime),

                    ArrivalTime =
                        ExtractClock(arrivalTime),

                    DepartureAirport =
                        string.IsNullOrWhiteSpace(departureAirport)
                            ? fromIata.ToUpperInvariant()
                            : departureAirport,

                    ArrivalAirport =
                        string.IsNullOrWhiteSpace(arrivalAirport)
                            ? toIata.ToUpperInvariant()
                            : arrivalAirport,

                    DurationText = durationText,

                    Status = status
                });
            }

            return cards;
        }

        private static string GetString(
            JsonElement parent,
            string propertyName)
        {
            if (!parent.TryGetProperty(
                    propertyName,
                    out var property))
            {
                return string.Empty;
            }

            if (property.ValueKind != JsonValueKind.String)
            {
                return string.Empty;
            }

            return property.GetString() ?? string.Empty;
        }

        private static string GetNestedString(
            JsonElement parent,
            string objectName,
            string propertyName)
        {
            if (!parent.TryGetProperty(
                    objectName,
                    out var objectElement))
            {
                return string.Empty;
            }

            if (!objectElement.TryGetProperty(
                    propertyName,
                    out var propertyElement))
            {
                return string.Empty;
            }

            if (propertyElement.ValueKind != JsonValueKind.String)
            {
                return string.Empty;
            }

            return propertyElement.GetString() ?? string.Empty;
        }

        private static string ExtractClock(string? value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                return string.Empty;
            }

            if (DateTimeOffset.TryParse(
                    value,
                    CultureInfo.InvariantCulture,
                    DateTimeStyles.None,
                    out var dateTime))
            {
                return dateTime.ToString("HH:mm");
            }

            return string.Empty;
        }

        private static string CalculateDuration(
            string departure,
            string arrival)
        {
            if (!DateTimeOffset.TryParse(
                    departure,
                    CultureInfo.InvariantCulture,
                    DateTimeStyles.None,
                    out var departureTime))
            {
                return string.Empty;
            }

            if (!DateTimeOffset.TryParse(
                    arrival,
                    CultureInfo.InvariantCulture,
                    DateTimeStyles.None,
                    out var arrivalTime))
            {
                return string.Empty;
            }

            var duration =
                arrivalTime - departureTime;

            if (duration.TotalMinutes < 0)
            {
                duration += TimeSpan.FromDays(1);
            }

            if (duration.TotalMinutes <= 0)
            {
                return string.Empty;
            }

            return
                $"{(int)duration.TotalHours} saat " +
                $"{duration.Minutes} dəq.";
        }
    }
}