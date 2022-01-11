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
   public class HoldingsController : ControllerBase
   {
      private readonly ILogger<HoldingsController> _logger;
      private readonly IHoldingsRepository _positionsRepository;

      public HoldingsController(ILogger<HoldingsController> logger, IHoldingsRepository positionsRepository)
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

      [HttpGet("opengrp/{ticker?}")]
      public async Task<IActionResult> GetTicker(string ticker)
      {
         var pos = await _positionsRepository.ListGroupByTickerAsync(ticker);

         return Ok(pos);
      }

      [HttpGet("open")]
      public async Task<IActionResult> GetOpenPositions()
      {
         var pos = await _positionsRepository.ListOpenAsync();

         return Ok(pos);
      }

      [HttpGet("open/{ticker}")]
      public async Task<IActionResult> GetOpenPositionsByTicker(string ticker)
      {
         var pos = await _positionsRepository.ListOpenByTickerAsync(ticker);

         return Ok(pos);
      }
   }
}
