using Newtonsoft.Json;

public class WeatherModel
{
    public string City { get; set; }
    public string Description { get; set; }
    public double Temperature { get; set; } // Ensure this is a double
    public double Humidity { get; set; }
    public double Pressure { get; set; }
    public double WindSpeed { get; set; }
    public double Latitude { get; set; }
    public double Longitude { get; set; }

    public static WeatherModel FromJson(string json)
    {
        dynamic data = JsonConvert.DeserializeObject(json);

        double rawTemperature = data.main.temp;
        Console.WriteLine($"Raw Temperature (Kelvin): {rawTemperature}");

        return new WeatherModel
        {
            City = data.name,
            Description = data.weather[0].description,
            Temperature = rawTemperature, // Convert from Kelvin to Celsius
            Humidity = data.main.humidity,
            Pressure = data.main.pressure,
            WindSpeed = data.wind.speed,
            Latitude = data.coord.lat,
            Longitude = data.coord.lon
        };
    }
}