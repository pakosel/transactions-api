using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using transactions_api.Models;

namespace transactions_api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PositionController : ControllerBase
    {
        private readonly ILogger<PositionController> _logger;
        private readonly MyWebApiContext _context;

        public PositionController(ILogger<PositionController> logger, MyWebApiContext context)
        {
            _logger = logger;
            _context = context;
        }

        [HttpGet("{date}")]
        public IEnumerable<TransactionsGroup> Get(DateTime date)
        {
            var trans = _context.Transactions
                .Where(t => t.Date <= date)
                .GroupBy(t => t.Stock, (s, tt) => new TransactionsGroup()
                {
                    Stock = s,
                    Quantity = tt.Sum(x => x.Quantity)
                })
                .Where(tg => tg.Quantity != 0)
                .ToList();

            return trans;
        }

        [HttpGet("{date}/{ticker}")]
        public IEnumerable<TransactionsGroup> GetTicker(DateTime date, string ticker)
        {
            var trans = _context.Transactions
                .Where(t => t.Date <= date && t.Stock.ToLower() == ticker.ToLower())
                .GroupBy(t => t.Stock, (s, tt) => new TransactionsGroup()
                {
                    Stock = s,
                    Quantity = tt.Sum(x => x.Quantity)
                }).ToList();

            return trans;
        }
    }
}
