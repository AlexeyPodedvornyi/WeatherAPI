using WeatherAPI.Models;

namespace WeatherAPI.Parsers.Interfaces
{
    public interface IJsonParser<TInput, TOutput>
    {
        TOutput ParseFromObject(TInput data);
        List<TOutput> ParseFromArray(TInput data);
        TInput ParseToJson(TOutput data);
        TInput ParseToJson(List<TOutput> data);
    }
}
