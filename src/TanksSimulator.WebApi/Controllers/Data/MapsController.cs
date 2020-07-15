using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TanksSimulator.Shared.Data;
using TanksSimulator.Shared.Models;
using TanksSimulator.WebApi.Services;

namespace TanksSimulator.WebApi.Controllers.Data
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/{version:apiVersion}/[controller]")]
    public class MapsController : ControllerBase
    {
        private readonly DataApiClient _dataApiClient;
        public MapsController(
            DataApiClient dataApiClient)
        {
            _dataApiClient = dataApiClient;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _dataApiClient.GetMaps();

            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var result = await _dataApiClient.GetMap(id);

            if (result == null)
            {
                return NotFound(new { error = "Could not find map." });
            }

            return Ok(result);
        }
    }
}
