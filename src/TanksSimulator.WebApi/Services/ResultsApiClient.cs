using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TanksSimulator.Shared.Models;
using TanksSimulator.WebApi.Controllers.Simulator.Models;

namespace TanksSimulator.WebApi.Services
{
    public class ResultsApiClient
    {
        private readonly HttpClient _client;

        public ResultsApiClient(HttpClient client)
        {
            _client = client;
        }

        public async Task<IEnumerable<GameDataModel>> GetScoresAsync()
        {
            var response = await _client.GetAsync("/api/score/");
            var content = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<IEnumerable<GameDataModel>>(content);

            return result;
        }

        public async Task<GameDataModel> GetScoreAsync(string id)
        {
            var response = await _client.GetAsync($"/api/score/{id}");
            var content = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<GameDataModel>(content);

            return result;
        }

        public async Task<GameDataModel> CreateAsync(GameDataModel model)
        {
            var content = new StringContent(JsonConvert.SerializeObject(new GameDataApiRequestModel(model)), Encoding.UTF8, "application/json");

            var response = await _client.PostAsync($"/api/score/", content);
            var responseContent = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
            {
                var responseResult = JsonConvert.DeserializeObject<GameDataModel>(responseContent);
                return responseResult;
            }

            return null;
        }

        public async Task<GameDataModel> UpdateAsync(GameDataModel model)
        {
            var content = new StringContent(JsonConvert.SerializeObject(new GameDataApiResponseModel(model)), Encoding.UTF8, "application/json");

            var response = await _client.PutAsJsonAsync($"/api/score/", content);
            var responseContent = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
            {
                var responseResult = JsonConvert.DeserializeObject<GameDataModel>(responseContent);
                return responseResult;
            }

            return null;
        }
    }
}
