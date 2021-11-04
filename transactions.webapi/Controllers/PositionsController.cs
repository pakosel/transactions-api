using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using transactions_api.Models;
using transactions_api.Interfaces;

namespace transactions_api.Controllers
{
   [ApiController]
   [Route("[controller]")]
   public class PositionsController : ControllerBase
   {
      private readonly ILogger<PositionsController> _logger;
      private readonly IPositionsRepository _positionsRepository;

      public PositionsController(ILogger<PositionsController> logger, IPositionsRepository positionsRepository)
      {
         _logger = logger;
         _positionsRepository = positionsRepository;
      }

      [HttpGet("{date}")]
      public async Task<IActionResult> Get(DateTime date)
      {
         var pos = await _positionsRepository.ListByDateAsync(date);

         return Ok(pos);
      }

      [HttpGet("opengrp")]
      public async Task<IActionResult> GetTicker()
      {
         var pos = await _positionsRepository.ListGroupByTickerAsync();

         return Ok(pos);
      }

      [HttpGet("open")]
      public async Task<IActionResult> GetOpenPostions()
      {
         var pos = await _positionsRepository.ListOpenAsync();

         return Ok(pos);
      }

      [HttpGet("open/{ticker}")]
      public async Task<IActionResult> GetOpenPostionsByTicker(string ticker)
      {
         var pos = await _positionsRepository.ListOpenByTickerAsync(ticker);

         return Ok(pos);
      }
   }
}
