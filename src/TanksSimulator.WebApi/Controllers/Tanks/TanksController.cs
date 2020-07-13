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
    [Route("api/[controller]")]
    [ApiController]
    public class TanksController : ControllerBase
    {
        private readonly TanksRepository _tanksRepository;

        public TanksController(
            TanksRepository tanksRepository)
        {
            _tanksRepository = tanksRepository;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_tanksRepository.Get());
        }

        [HttpPost]
        public IActionResult Create([FromBody] TankModel model)
        {
            var result = _tanksRepository.Create(model);

            return Accepted(result);
        }
    }
}
