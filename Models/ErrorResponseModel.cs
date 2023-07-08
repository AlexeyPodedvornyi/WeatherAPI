using Swagger.Net.Annotations;

namespace WeatherAPI.Models
{
    public class ErrorResponseModel
    {
        public string Type { get; set; }
        public string Message { get; set; }
        public int StatusCode { get; set; }
    }
}
