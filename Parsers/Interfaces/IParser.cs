namespace WeatherAPI.Parsers.Interfaces
{
    public interface IParser<TInput, TOutput>
    {
        TOutput ParseFrom(TInput data);
        TInput ParseTo(TOutput data);
    }
}
