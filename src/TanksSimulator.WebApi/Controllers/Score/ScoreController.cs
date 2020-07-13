using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TanksSimulator.WebApi.Data;

namespace TanksSimulator.WebApi.Controllers.Score
{
    [Route("api/[controller]")]
    [ApiController]
    public class ScoreController : ControllerBase
    {
        private readonly GameDataRepository _gameDataRepository;

        public ScoreController(
            GameDataRepository gameDataRepository)
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
        public async Task<IActionResult> GetAll(string id)
        {
            var result = await _gameDataRepository
                .GetByIdAsync(id);

            if (result == null)
            {
                return NotFound(new { error = "Could not find any data for this battle." });
            }

            return Ok(result);
        }

    }
}
