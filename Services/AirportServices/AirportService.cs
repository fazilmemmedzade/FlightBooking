using FlightBooking.Models;
using System.Text.Json;

namespace FlightBooking.Services.AirportServices
{
    public class AirportService : IAirportService
    {
        private readonly HttpClient _client;

        public AirportService(HttpClient client)
        {
            _client = client;
        }

        public async Task<List<AirportResult>> SearchAirportsAsync(string query)
        {
            var results = new List<AirportResult>();

            if (string.IsNullOrWhiteSpace(query))
            {
                return results;
            }

            var requestUrl =
                $"https://api.freeairportdb.com/v1/airports" +
                $"?q={Uri.EscapeDataString(query.Trim())}" +
                $"&limit=20";

            using var response = await _client.GetAsync(requestUrl);

            response.EnsureSuccessStatusCode();

            var body = await response.Content.ReadAsStringAsync();

            var parsed = JsonSerializer.Deserialize<FreeAirportDbResponse>(
                body,
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

            if (parsed?.Data == null)
            {
                return results;
            }

            foreach (var airport in parsed.Data)
            {
                if (string.IsNullOrWhiteSpace(airport.Iata))
                {
                    continue;
                }

                results.Add(new AirportResult
                {
                    Iata = airport.Iata,
                    AirportName = airport.Name ?? "",
                    City = airport.Municipality ?? "",
                    Title = airport.Name ?? "",
                    Type = airport.Type ?? ""
                });
            }

            return results;
        }

        public async Task<AirportResult?> GetFirstIataAsync(string query)
        {
            var list = await SearchAirportsAsync(query);

            if (list.Count == 0)
            {
                return null;
            }

            var searchText = query.Trim();

            return list
                .OrderByDescending(airport =>
                    airport.City.Equals(
                        searchText,
                        StringComparison.OrdinalIgnoreCase))
                .ThenByDescending(airport =>
                    GetAirportTypePriority(airport.Type))
                .ThenByDescending(airport =>
                    airport.AirportName.Contains(
                        searchText,
                        StringComparison.OrdinalIgnoreCase))
                .FirstOrDefault();
        }

        private static int GetAirportTypePriority(string? type)
        {
            return type?.ToLowerInvariant() switch
            {
                "large_airport" => 3,
                "medium_airport" => 2,
                "small_airport" => 1,
                _ => 0
            };
        }

        private static int GetAirportTypePriority(AirportResult airport)
        {
            return 0;
        }

    }

    public class FreeAirportDbResponse
    {
        public List<FreeAirportDbAirport> Data { get; set; } = [];
        public int Total { get; set; }
        public int Limit { get; set; }
        public int Offset { get; set; }
    }

    public class FreeAirportDbAirport
    {
        public string? Code { get; set; }
        public string? Iata { get; set; }
        public string? Icao { get; set; }
        public string? Name { get; set; }
        public string? CountryCode { get; set; }
        public string? CountryName { get; set; }
        public string? Municipality { get; set; }
        public string? Type { get; set; }
        public double? Latitude { get; set; }
        public double? Longitude { get; set; }
    }
}