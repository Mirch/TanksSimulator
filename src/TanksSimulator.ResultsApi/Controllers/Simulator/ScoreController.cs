using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TanksSimulator.ResultsApi.Controllers.Simulator.Models;
using TanksSimulator.Shared.Data;
using TanksSimulator.Shared.Models;

namespace TanksSimulator.ResultsApi.Controllers.Simulator
{
    [ApiController]
    [Route("api/[controller]")]
    public class ScoreController : ControllerBase
    {
        private readonly IRepository<GameDataModel> _gameDataRepository;

        public ScoreController(
            IRepository<GameDataModel> gameDataRepository)
        {
            _gameDataRepository = gameDataRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _gameDataRepository
                .GetAsync();

            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            var result = await _gameDataRepository
                .GetByIdAsync(id);

            if (result == null)
            {
                return NotFound(new { error = "Could not find any data for this battle." });
            }

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] GameDataApiRequestModel model)
        {
            var result = await _gameDataRepository
                .CreateAsync(new GameDataModel
                {
                    Tank1Id = model.Tank1Id,
                    Tank2Id = model.Tank2Id,
                    MapId = model.MapId,
                    Status = model.Status,
                    WinnerId = model.WinnerId,
                    Logs = model.Logs,
                });

            return Ok(result);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] GameDataApiRequestModel model)
        {
            var result = await _gameDataRepository
                .UpdateAsync(new GameDataModel
                {
                    Id = model.Id,
                    Tank1Id = model.Tank1Id,
                    Tank2Id = model.Tank2Id,
                    MapId = model.MapId,
                    Status = model.Status,
                    WinnerId = model.WinnerId,
                    Logs = model.Logs,
                });

            return Ok(result);
        }

    }
}
