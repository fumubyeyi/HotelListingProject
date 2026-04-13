using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HotelListing.Api.Services;

namespace HotelListing.Api.Weather;
public class WeatherForecastEndpoints
{
    private readonly IWeatherForecastService _weatherForecastService;

    public WeatherForecastEndpoints(IWeatherForecastService weatherForecastService)
    {
        _weatherForecastService = weatherForecastService;
    }
    public void MapWeatherForecastEndpoints(WebApplication app)
    {
        app.MapGet("/weather", () => _weatherForecastService.Get())
            .WithName("GetWeatherForecast")
            .WithTags("Weather")
            .WithSummary("Gets the weather forecast for the next 5 days.")
            .WithDescription("This endpoint returns a list of weather forecasts for the next 5 days, including the date, temperature, and a summary of the weather conditions.");
    }
}
