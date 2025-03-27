using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using WeatherApp.Models;

namespace WeatherApp.Controllers
{
    public class WeatherController : Controller
    {
        private readonly HttpClient _httpClient;

        public WeatherController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IActionResult> Index(string city, double? lat, double? lon)
        {
            if (string.IsNullOrEmpty(city) && !lat.HasValue && !lon.HasValue)
            {
                return View();
            }

            var apiKey = "9c3266d374dad39db6a0c935e090a08d"; // API key from OpenWeatherMap.
            string url;

            if (!string.IsNullOrEmpty(city))
            {
                // Use city name to get weather data
                url = $"https://api.openweathermap.org/data/2.5/weather?q={city}&appid={apiKey}&units=metric";
            }
            else if (lat.HasValue && lon.HasValue)
            {
                // Use latitude and longitude to get weather data
                url = $"https://api.openweathermap.org/data/2.5/weather?lat={lat.Value}&lon={lon.Value}&appid={apiKey}&units=metric";
            }
            else
            {
                return View(); // Return view if no valid input is provided
            }

            var response = await _httpClient.GetStringAsync(url);
            var weatherData = WeatherModel.FromJson(response); // Use the FromJson method
            return View(weatherData);
        }
    }
}