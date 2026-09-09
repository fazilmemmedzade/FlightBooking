using FlightBooking.Dtos.FlightSearchDtos;
using FlightBooking.Services.AirportServices;
using FlightBooking.Services.AirportServices.FlightSearchServices;
using Microsoft.AspNetCore.Mvc;

namespace FlightBooking.Controllers
{
    public class FlightController : Controller
    {
        private readonly IAirportService _airportService;
        private readonly IFlightSearchService _flightSearchService;

        public FlightController(
            IAirportService airportService,
            IFlightSearchService flightSearchService)
        {
            _airportService = airportService;
            _flightSearchService = flightSearchService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Search()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> SearchAirport(
            string query)
        {
            if (string.IsNullOrWhiteSpace(query))
            {
                return Json(new List<object>());
            }

            try
            {
                var airports =
                    await _airportService.SearchAirportsAsync(query);

                return Json(airports);
            }
            catch (Exception ex)
            {
                return StatusCode(
                    500,
                    new
                    {
                        error = ex.Message
                    });
            }
        }

        [HttpGet]
        public async Task<IActionResult> ResolveIata(
            string fromCity,
            string toCity)
        {
            var from =
                await _airportService.GetFirstIataAsync(fromCity);

            var to =
                await _airportService.GetFirstIataAsync(toCity);

            return Json(new
            {
                fromIata = from?.Iata,
                fromAirport = from?.AirportName,
                toIata = to?.Iata,
                toAirport = to?.AirportName
            });
        }

        [HttpGet]
        public async Task<IActionResult> SearchFlights(
            string fromCity,
            string toCity,
            string fromIata,
            string toIata,
            string depart,
            int adults = 1,
            string cabin = "economy",
            string currency = "AZN")
        {
            var result = new FlightSearchResultDto
            {
                Currency = currency
            };

            try
            {
                var from =
                    string.IsNullOrWhiteSpace(fromIata)
                        ? await _airportService
                            .GetFirstIataAsync(fromCity)
                        : new Models.AirportResult
                        {
                            Iata = fromIata,
                            AirportName = fromCity
                        };

                var to =
                    string.IsNullOrWhiteSpace(toIata)
                        ? await _airportService
                            .GetFirstIataAsync(toCity)
                        : new Models.AirportResult
                        {
                            Iata = toIata,
                            AirportName = toCity
                        };

                if (string.IsNullOrWhiteSpace(from?.Iata) ||
                    string.IsNullOrWhiteSpace(to?.Iata))
                {
                    result.Success = false;

                    result.Error =
                        "Gediş və ya təyinat üçün hava limanı tapılmadı.";

                    return Json(result);
                }

                result.FromIata = from.Iata;
                result.FromAirport = from.AirportName;

                result.ToIata = to.Iata;
                result.ToAirport = to.AirportName;

                result.Flights =
                    await _flightSearchService.SearchAsync(
                        from.Iata,
                        to.Iata,
                        depart,
                        adults,
                        cabin,
                        currency);

                result.Success = true;

                return Json(result);
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Error = ex.Message;

                return Json(result);
            }
        }
    }
}