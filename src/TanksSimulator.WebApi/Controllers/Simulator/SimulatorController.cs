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
    [Route("api/simulate")]
    [ApiController]
    public class SimulatorController : ControllerBase
    {
        private readonly GameSimulatorService _gameSimulator;
        private readonly GameDataRepository _gameDataRepository;
        private readonly TanksRepository _tanksRepository;
        private readonly MapsRepository _mapsRepository;


        public SimulatorController(
            GameSimulatorService gameSimulator,
            GameDataRepository gameDataRepository,
            TanksRepository tanksRepository,
            MapsRepository mapsRepository)
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

            return Ok(gameData);
        }
    }
}
