using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Swagger.Net.Annotations;
using WeatherAPI.Models.Responses.Interfaces;

namespace WeatherAPI.Models.Responses
{
    public class ErrorResponseModel : IResponseModel
    {
        public int StatusCode { get; set; }
        public string Type { get; set; }
        public string Message { get; set; }           
    }
}
