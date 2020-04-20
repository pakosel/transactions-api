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
    public class TransactionsController : ControllerBase
    {
        private readonly ILogger<TransactionsController> _logger;
        private readonly MyWebApiContext _context;

        public TransactionsController(ILogger<TransactionsController> logger, MyWebApiContext context)
        {
            _logger = logger;
            _context = context;
        }

        [HttpGet]
        public IEnumerable<Transactions> Get()
        {
            var trans = _context.Transactions.ToList();

            return trans;
        }

        [HttpGet("{ticker}")]
        public IEnumerable<Transactions> GetTicker(string ticker)
        {
            var trans = _context.Transactions.Where(t => t.Stock == ticker).ToList();

            return trans;
        }
    }
}
