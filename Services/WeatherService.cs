using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using MyWebApi.Models;
using Microsoft.Extensions.Configuration;

namespace MyWebApi.Services
{

    public interface IWeatherService
    {
        Task<WeatherData> GetWeatherDataAsync(string city);
    }

    public class WeatherService
    {
        private readonly IConfiguration _configuration;
        private readonly HttpClient _httpClient;

        public WeatherService(IConfiguration configuration, HttpClient httpClient)
        {
            _configuration = configuration;
            _httpClient = httpClient;
        }

        public async Task<WeatherData> GetWeatherAsync(string cityName)
        {
            var apiKey = _configuration.GetValue<string>("OpenWeatherMapApiKey");
            var url = $"https://api.openweathermap.org/data/2.5/weather?q={cityName}&appid={apiKey}&units=metric";
            var response = await _httpClient.GetAsync(url);

            if (!response.IsSuccessStatusCode)
            {
                return null;
            }

            var responseContent = await response.Content.ReadAsStringAsync();
            var weatherData = JsonSerializer.Deserialize<WeatherData>(responseContent);

            return weatherData;
        }
    }
}
