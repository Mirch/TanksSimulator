using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TanksSimulator.Shared.Models;
using TanksSimulator.WebApi.Controllers.Simulator.Models;
using TanksSimulator.WebApi.Data;

namespace TanksSimulator.WebApi.Controllers.Simulator
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{apiVersion:apiVersion}/[controller]")]
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
            var result = (await _gameDataRepository
                .GetAsync())
                .Select(d => new GameDataApiResponseModel(d));

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

            return Ok(new GameDataApiResponseModel(result));
        }

    }
}
