namespace WeatherAPI.Services.Interfaces
{
    public interface IWeatherForecastService
    {
        Task<string> GetCurrentWeatherAsync(string cityName);

    }
}
