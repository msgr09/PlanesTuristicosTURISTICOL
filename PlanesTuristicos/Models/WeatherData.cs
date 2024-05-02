using NuGet.Versioning;

namespace PlanesTuristicos.Models
{

    public class WeatherData
    {
        public LocationData Location { get; set; }
        public CurrentWeatherData Current { get; set; }
    }

    public class LocationData
    {
        public string Name { get; set; }
        public string Country { get; set; }
    }

    public class CurrentWeatherData
    {
        public float Temperature { get; set; }
        public float Humidity { get; set; }
        
    }

}
