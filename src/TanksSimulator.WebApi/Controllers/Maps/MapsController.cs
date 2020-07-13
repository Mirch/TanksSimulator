using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TanksSimulator.WebApi.Data;

namespace TanksSimulator.WebApi.Controllers.Maps
{
    [Route("api/[controller]")]
    [ApiController]
    public class MapsController : ControllerBase
    {
        private readonly MapsRepository _mapsRepository;

        public MapsController(
            MapsRepository mapsRepository)
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
        public IActionResult GetById(string id)
        {
            var result = _mapsRepository.GetByIdAsync(id);

            if (result == null)
            {
                return NotFound(new { error = "Could not find map." });
            }

            return Ok(result);
        }
    }
}
