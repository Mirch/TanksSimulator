using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson.Serialization;
using TanksSimulator.Game;
using TanksSimulator.Game.Map;
using TanksSimulator.Shared.Models;
using TanksSimulator.WebApi.Controllers.Simulator.Models;
using TanksSimulator.WebApi.Data;
using TanksSimulator.WebApi.Services;

namespace TanksSimulator.WebApi.Controllers.Simulator
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{apiVersion:apiVersion}/simulate")]
    public class SimulatorController : ControllerBase
    {
        private readonly GameSimulatorService _gameSimulator;
        private readonly IRepository<GameDataModel> _gameDataRepository;
        private readonly IRepository<TankModel> _tanksRepository;
        private readonly IRepository<GameMapModel> _mapsRepository;


        public SimulatorController(
            GameSimulatorService gameSimulator,
            IRepository<GameDataModel> gameDataRepository,
            IRepository<TankModel> tanksRepository,
            IRepository<GameMapModel> mapsRepository)
        {
            _gameSimulator = gameSimulator;
            _gameDataRepository = gameDataRepository;
            _tanksRepository = tanksRepository;
            _mapsRepository = mapsRepository;
        }

        [HttpPost]
        public async Task<IActionResult> Simulate([FromBody] SimulateApiRequestModel request)
        {
            var tank1 = await _tanksRepository.GetByIdAsync(request.Tanks[0]);
            var tank2 = await _tanksRepository.GetByIdAsync(request.Tanks[1]);
            var map = await _mapsRepository.GetByIdAsync(request.MapId);

            if (tank1 == null || tank2 == null || map == null)
            {
                return NotFound(new { error = "Wrong tank or map id provided." });
            }

            var gameData = await _gameDataRepository.CreateAsync(new GameDataModel
            {
                Tank1Id = request.Tanks[0],
                TankModel1 = tank1,
                Tank2Id = request.Tanks[1],
                TankModel2 = tank2,
                MapId = request.MapId,
                GameMapModel = map,
                Status = GameStatus.InQueue
            });

            var result = await _gameSimulator.SimulateAsync(gameData.Id);

            return Ok(new GameDataApiResponseModel(result));
        }
    }
}
