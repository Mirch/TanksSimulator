using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TanksSimulator.Shared.Data;
using TanksSimulator.Shared.Models;

namespace TanksSimulator.DataApi.Controllers.Maps
{
    [ApiController]
    [Route("api/[controller]")]
    public class MapsController : ControllerBase
    {
        private readonly IRepository<GameMapModel> _mapsRepository;

        public MapsController(
            IRepository<GameMapModel> mapsRepository)
        {
            _mapsRepository = mapsRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _mapsRepository.GetAsync();

            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var result = await _mapsRepository.GetByIdAsync(id);

            if (result == null)
            {
                return NotFound(new { error = "Could not find map." });
            }

            return Ok(result);
        }
    }
}
