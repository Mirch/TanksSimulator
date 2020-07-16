using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TanksSimulator.Shared.Data;
using TanksSimulator.Shared.Models;
using TanksSimulator.WebApi.Controllers.Simulator.Models;
using TanksSimulator.WebApi.Services;

namespace TanksSimulator.ResultsApi.Controllers.Simulator
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/{version:apiVersion}/[controller]")]
    public class ScoreController : ControllerBase
    {
        private readonly ResultsApiClient _resultsApiClient;

        public ScoreController(
            ResultsApiClient resultsApiClient)
        {
            _resultsApiClient = resultsApiClient;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = (await _resultsApiClient
                .GetScoresAsync())
                .Select(d => new GameDataApiResponseModel(d));

            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            var result = await _resultsApiClient
                .GetScoreAsync(id);

            if (result == null)
            {
                return NotFound(new { error = "Could not find any data for this battle." });
            }

            return Ok(new GameDataApiResponseModel(result));
        }
    }
}
