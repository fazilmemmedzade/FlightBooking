using FlightBooking.Models;

namespace FlightBooking.Services.AirportServices
{
    public interface IAirportService
    {
        Task<List<AirportResult>> SearchAirportsAsync(string query);
        Task<AirportResult?> GetFirstIataAsync(string query);
    }
}
