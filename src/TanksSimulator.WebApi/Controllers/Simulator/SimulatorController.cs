using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TanksSimulator.Game;
using TanksSimulator.Game.Map;
using TanksSimulator.Shared.Models;
using TanksSimulator.WebApi.Controllers.Simulator.Models;
using TanksSimulator.WebApi.Data;
using TanksSimulator.WebApi.Services;

namespace TanksSimulator.WebApi.Controllers.Simulator
{
    [Route("api/simulate")]
    [ApiController]
    public class SimulatorController : ControllerBase
    {
        private readonly GameSimulatorService _gameSimulator;

        public SimulatorController(
            GameSimulatorService gameSimulator)
        {
            _gameSimulator = gameSimulator;
        }

        [HttpPost]
        public async Task<IActionResult> Simulate([FromBody] SimulateApiRequestModel request)
        {
            var result = _gameSimulator.Simulate();

            return Ok(result);
        }
    }
}
