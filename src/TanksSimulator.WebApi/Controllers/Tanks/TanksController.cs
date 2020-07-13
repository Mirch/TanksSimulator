﻿using System;
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
        public async Task<IActionResult> GetAll()
        {
            var result = await _tanksRepository.GetAsync();

            return Ok(result);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(string id)
        {
            var result = _tanksRepository.GetByIdAsync(id);

            if (result == null)
            {
                return NotFound(new { error = "Could not find tank." });
            }

            return Ok(result);
        }

    }
}
