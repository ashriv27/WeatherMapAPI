using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MyWebApi.Models;
using MyWebApi.Services;

namespace MyWebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherController : ControllerBase
    {
        private readonly WeatherService _weatherService;

        public WeatherController(WeatherService weatherService)
        {
            _weatherService = weatherService;
        }

        [HttpGet("{cityName}")]
        public async Task<ActionResult<WeatherData>> Get(string cityName)
        {
            var weatherData = await _weatherService.GetWeatherAsync(cityName);

            if (weatherData == null)
            {
                return NotFound();
            }

            return weatherData;
        }
    }
}
