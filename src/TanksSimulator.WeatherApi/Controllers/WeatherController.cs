using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace TanksSimulator.WeatherApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "raining", "freezing", "sunny"
        };

        [HttpGet]
        public IActionResult GetWeather()
        {
            var random = new Random();
            var weather = Summaries[random.Next(Summaries.Length)];

            return Ok(weather);
        }
    }
}
