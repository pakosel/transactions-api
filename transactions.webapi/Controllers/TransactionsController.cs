using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using transactions_api.Interfaces;
using AutoMapper;
using transactions_api.Dto;

namespace transactions_api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TransactionsController : ControllerBase
    {
        private readonly ILogger<TransactionsController> _logger;
        private readonly ITransactionsRepository _transactionsRepository;
        private readonly IMapper _mapper;

        public TransactionsController(ILogger<TransactionsController> logger, ITransactionsRepository transactionsRepository, IMapper mapper)
        {
            _logger = logger;
            _transactionsRepository = transactionsRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var trans = await _transactionsRepository.ListAsync();

            return Ok(_mapper.Map<List<TransactionReadDto>>(trans));
        }

        [HttpGet("{ticker}")]
        public async Task<IActionResult> GetTicker(string ticker)
        {
            var trans = await _transactionsRepository.ListByTickerAsync(ticker);
            if(trans != null)
                return Ok(_mapper.Map<List<TransactionReadDto>>(trans));
            else
                return NotFound();
        }
    }
}
