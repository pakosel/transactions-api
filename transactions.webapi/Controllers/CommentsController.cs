using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using transactions_api.Models;
using transactions_api.Interfaces;
using transactions_api.Dto;
using AutoMapper;

namespace transactions_api.Controllers
{
   [ApiController]
   [Route("[controller]")]
   public class CommentsController : ControllerBase
   {
      private readonly ILogger<CommentsController> _logger;
      private readonly ICommentRepository _commentsRepository;
      private readonly IMapper _mapper;


      public CommentsController(ILogger<CommentsController> logger, ICommentRepository commentsRepository, IMapper mapper)
      {
         _logger = logger;
         _commentsRepository = commentsRepository;
         _mapper = mapper;
      }

      [HttpGet]
      public async Task<IActionResult> Get()
      {
         var pos = await _commentsRepository.ListAsync();

         return Ok(pos);
      }

      [HttpPost("add")]
      public async Task<IActionResult> AddTransaction([FromBody] CommentReadDto commentDto)
      {
         //_logger.LogInformation($"Transactions ADD {transactionDto}");
         var comment = _mapper.Map<Comment>(commentDto);
         await _commentsRepository.AddAsync(comment);
         return Ok(comment);
      }

      [HttpGet("{transactionId}")]
      public async Task<IActionResult> GetCommentsByTranId(int transactionId)
      {
         var pos = await _commentsRepository.ListByTransactionIdAsync(transactionId);

         return Ok(pos);
      }
   }
}
