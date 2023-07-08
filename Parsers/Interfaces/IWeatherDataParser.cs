using WeatherAPI.Models;

namespace WeatherAPI.Parsers.Interfaces
{
    public interface IWeatherDataParser : IJsonParser<string, WeatherForecast>
    {
    }
}
