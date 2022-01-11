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
      private readonly IProfitRepository _profitRepository;
      private readonly IMapper _mapper;

      public TransactionsController(ILogger<TransactionsController> logger, ITransactionsRepository transactionsRepository, IStocksLeftRepository stocksLeftRepository, IProfitRepository profitRepository, IMapper mapper)
      {
         _logger = logger;
         _transactionsRepository = transactionsRepository;
         _stocksLeftRepository = stocksLeftRepository;
         _profitRepository = profitRepository;
         _mapper = mapper;
      }

      [HttpGet]
      public async Task<IActionResult> Get()
      {
         //_logger.LogInformation("Transactions GET");
         var trans = await _transactionsRepository.ListAsync();

         return Ok(_mapper.Map<List<TransactionReadDto>>(trans));
      }

      [HttpGet("{ticker}")]
      public async Task<IActionResult> GetTicker(string ticker)
      {
         //_logger.LogInformation($"Transactions GET ticker={ticker}");

         var trans = await _transactionsRepository.ListByTickerAsync(ticker);
         if (trans != null)
            return Ok(_mapper.Map<List<TransactionReadDto>>(trans));
         else
            return NotFound();
      }

      [HttpPost("Add")]
      public async Task<IActionResult> AddTransaction([FromBody] TransactionReadDto transactionDto)
      {
         //_logger.LogInformation($"Transactions ADD {transactionDto}");
         var transaction = _mapper.Map<Transaction>(transactionDto);

         if (transactionDto.Operation == TransactionOperation.BUY.ToString())
            return await RegisterBuy(transaction);
         if (transactionDto.Operation == TransactionOperation.SELL.ToString())
            return await RegisterSell(transaction);
         else
         {
            var err = $"Unsupported transaction operation: {transactionDto.Operation}";
            _logger.LogError(err);
            return NotFound(err);
         }
      }

      private async Task<IActionResult> RegisterBuy(Transaction transaction)
      {
         await _transactionsRepository.AddAsync(transaction);
         await _stocksLeftRepository.AddAsync(transaction.TransactionId, transaction.Quantity);
         return Ok(transaction);
      }

      private async Task<IActionResult> RegisterSell(Transaction transaction)
      {
         var remainingSellQuantity = transaction.Quantity;
         var remainingStocks = _stocksLeftRepository.ListRemainingAsync(transaction.Stock).Result;
         if (remainingStocks.Sum(rs => rs.Quantity) < remainingSellQuantity)
         {
            var err = $"Not enough stock quantity in the wallet for Stock {transaction.Stock} and Quantity {transaction.Quantity}";
            _logger.LogError(err);
            return NotFound(err);
         }

         await _transactionsRepository.AddAsync(transaction);
         foreach (var remainingStock in remainingStocks)
         {
            var deducting = Math.Min(remainingStock.Quantity, remainingSellQuantity);
            var buyTran = _transactionsRepository.GetByIdAsync(remainingStock.TransactionId).Result;
            var partBuy = deducting == buyTran.Quantity ? 1 : (decimal)(deducting / buyTran.Quantity);
            var partSell = deducting == transaction.Quantity ? 1 : (decimal)(deducting / transaction.Quantity);
            var profitAmount = (decimal) (partSell * (transaction.Amount - transaction.Commision) -
                                          partBuy * (buyTran.Amount + buyTran.Commision));
            //_logger.LogInformation($"PartBuy = {partBuy}");
            //_logger.LogInformation($"PartSell = {partSell}");
            //_logger.LogInformation($"profitAmount = {profitAmount}");
            await _stocksLeftRepository.UpdateAsync(remainingStock, remainingStock.Quantity - deducting);
            await _profitRepository.AddAsync(buyTran, transaction, partBuy, partSell, profitAmount, deducting, transaction.AmountCurrencySymbol);
            remainingSellQuantity -= deducting;
            if (remainingSellQuantity <= 0)
               break;
         }

         return Ok(transaction);
      }
   }
}
