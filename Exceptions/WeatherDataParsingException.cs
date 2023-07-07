using System.Runtime.Serialization;

namespace WeatherAPI.Exceptions
{
    [Serializable]
    public class WeatherDataParsingException : Exception
    {
        public WeatherDataParsingException() :base()
        {            
        }

        public WeatherDataParsingException(string message) : base(message)
        {            
        }

        public WeatherDataParsingException(string message, Exception innerException) : base(message, innerException)
        {         
        }

        protected WeatherDataParsingException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
