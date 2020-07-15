using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using TanksSimulator.Shared.Models;

namespace TanksSimulator.WebApi.Services
{
    public class DataApiClient
    {
        private readonly HttpClient _client;

        public DataApiClient(HttpClient client)
        {
            _client = client;
        }

        public async Task<IEnumerable<TankModel>> GetTanks()
        {
            var response = await _client.GetAsync("/api/tanks/");
            var content = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<IEnumerable<TankModel>>(content);

            return result;
        }

        public async Task<TankModel> GetTank(string id)
        {
            var response = await _client.GetAsync($"/api/tanks/{id}");
            var content = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<TankModel>(content);

            return result;
        }

        public async Task<IEnumerable<GameMapModel>> GetMaps()
        {
            var response = await _client.GetAsync("/api/maps/");
            var content = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<IEnumerable<GameMapModel>>(content);

            return result;
        }

        public async Task<GameMapModel> GetMap(string id)
        {
            var response = await _client.GetAsync($"/api/maps/{id}");
            var content = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<GameMapModel>(content);

            return result;
        }

    }
}
