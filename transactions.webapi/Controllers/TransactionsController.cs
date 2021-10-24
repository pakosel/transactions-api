using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using transactions_api.Interfaces;
using AutoMapper;
using transactions_api.Dto;
using transactions_api.Models;

namespace transactions_api.Controllers
{
   [ApiController]
   [Route("[controller]")]
   public class TransactionsController : ControllerBase
   {
      private readonly ILogger<TransactionsController> _logger;
      private readonly ITransactionsRepository _transactionsRepository;
      private readonly IStocksLeftRepository _stocksLeftRepository;
      private readonly IMapper _mapper;

      public TransactionsController(ILogger<TransactionsController> logger, ITransactionsRepository transactionsRepository, IStocksLeftRepository stocksLeftRepository, IMapper mapper)
      {
         _logger = logger;
         _transactionsRepository = transactionsRepository;
         _stocksLeftRepository = stocksLeftRepository;
         _mapper = mapper;
      }

      [HttpGet]
      public async Task<IActionResult> Get()
      {
         _logger.LogInformation("Transations GET");
         var trans = await _transactionsRepository.ListAsync();

         return Ok(_mapper.Map<List<TransactionReadDto>>(trans));
      }

      [HttpGet("{ticker}")]
      public async Task<IActionResult> GetTicker(string ticker)
      {
         _logger.LogInformation($"Transations GET ticker={ticker}");

         var trans = await _transactionsRepository.ListByTickerAsync(ticker);
         if (trans != null)
            return Ok(_mapper.Map<List<TransactionReadDto>>(trans));
         else
            return NotFound();
      }

      [HttpPost("Add")]
      public async Task<IActionResult> AddTransaction([FromBody] TransactionReadDto transactionDto)
      {
         _logger.LogInformation($"Transations ADD {transactionDto}");
         if(transactionDto.Operation == TransactionOperation.BUY.ToString() ||
            transactionDto.Operation == TransactionOperation.SELL.ToString())
         {
            var transaction = _mapper.Map<Transaction>(transactionDto);
            await _transactionsRepository.AddAsync(transaction);
            if (transactionDto.Operation == TransactionOperation.BUY.ToString())
               await _stocksLeftRepository.AddAsync(transaction.TransactionId, transaction.Quantity);
            //if (transactionDto.Operation == TransactionOperation.SELL.ToString())
               //TODO

            return Ok(transaction);
         }
         else
            return NotFound($"Unsupported transaction operation: {transactionDto.Operation}");
      }
   }
}
