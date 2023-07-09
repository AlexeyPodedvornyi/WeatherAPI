using Swashbuckle.AspNetCore.Filters;

namespace WeatherAPI.Models.Responses
{
    public class ErrorResponseModel500Example : ErrorResponseModel, IExamplesProvider<ErrorResponseModel500Example>
    {
        public ErrorResponseModel500Example()
        {
            StatusCode = 500;
            Type = "InternalServerError";
            Message = "Server error: {details}";
        }
        public ErrorResponseModel500Example(string details) : this()
        {
            Message = $"Server error: {details}";
        }
        public ErrorResponseModel500Example GetExamples()
        {
            return new ErrorResponseModel500Example();
        }
    }
}
