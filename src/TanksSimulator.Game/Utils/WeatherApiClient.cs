using MongoDB.Bson.IO;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace TanksSimulator.Game.Utils
{
    public class WeatherApiClient
    {
        private readonly HttpClient _client;

        public WeatherApiClient(string uri)
        {
            _client = new HttpClient();
            _client.BaseAddress = new Uri(uri);
        }

        public async Task<string> GetWeatherAsync()
        {
            var response = await _client.GetAsync("/api/weather/");
            var content = await response.Content.ReadAsStringAsync();

            return content.Trim('"');
        }
    }
}
