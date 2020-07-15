using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TanksSimulator.Shared.Models;
using TanksSimulator.WebApi.Data;

namespace TanksSimulator.WebApi.Controllers.Tanks
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{apiVersion:apiVersion}/[controller]")]
    public class TanksController : ControllerBase
    {
        private readonly IRepository<TankModel> _tanksRepository;

        public TanksController(
            IRepository<TankModel> tanksRepository)
        {
            _tanksRepository = tanksRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _tanksRepository.GetAsync();

            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var result = await _tanksRepository.GetByIdAsync(id);

            if (result == null)
            {
                return NotFound(new { error = "Could not find tank." });
            }

            return Ok(result);
        }

    }
}
