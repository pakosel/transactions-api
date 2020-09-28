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
    public class TickersController : ControllerBase
    {
        private readonly ILogger<PositionsController> _logger;
        private readonly ITransactionsRepository _transactionsRepository;

        public TickersController(ILogger<PositionsController> logger, ITransactionsRepository transactionsRepository)
        {
            _logger = logger;
            _transactionsRepository = transactionsRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var pos = await _transactionsRepository.ListTickersAsync();

            return Ok(pos);
        }
    }
}
