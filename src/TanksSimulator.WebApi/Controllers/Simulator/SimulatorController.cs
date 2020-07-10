using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TanksSimulator.Game;
using TanksSimulator.Game.Map;
using TanksSimulator.Shared.Models;
using TanksSimulator.WebApi.Controllers.Simulator.Models;
using TanksSimulator.WebApi.Data;

namespace TanksSimulator.WebApi.Controllers.Simulator
{
    [Route("api/simulate")]
    [ApiController]
    public class SimulatorController : ControllerBase
    {
        private readonly GameDataRepository _gameDataRepository;

        public SimulatorController(
            GameDataRepository gameDataRepository)
        {
            _gameDataRepository = gameDataRepository;
        }

        [HttpPost]
        public async Task<IActionResult> Simulate([FromBody] SimulateApiRequestModel request)
        {
            TankModel tank1 = new TankModel(); // from request
            TankModel tank2 = new TankModel(); // from request
            GameMapModel map = new GameMapModel()
            {
                Id = 1,
            };

            GameData gameData = new GameData
            {
                MapId = map.Id,
                Tank1Id = tank1.Id,
                Tank2Id = tank2.Id
            };

            _gameDataRepository.Insert(gameData);

            GameSimulator simulator = new GameSimulator(map, tank1, tank2);

            simulator.Start(); // in a thread

            return Ok(gameData);
        }
    }
}
