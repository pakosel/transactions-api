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
    public class TransactionsController : ControllerBase
    {
        private readonly ILogger<TransactionsController> _logger;
        private readonly ITransactionsRepository _transactionsRepository;

        public TransactionsController(ILogger<TransactionsController> logger, ITransactionsRepository transactionsRepository)
        {
            _logger = logger;
            _transactionsRepository = transactionsRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var trans = await _transactionsRepository.ListAsync();

            return Ok(trans);
        }

        [HttpGet("{ticker}")]
        public async Task<IActionResult> GetTicker(string ticker)
        {
            var trans = await _transactionsRepository.ListByTickerAsync(ticker);

            return Ok(trans);
        }
    }
}
