using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Comment;
using api.Interfraces;
using api.Mappers;
using api.Repository;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [Route("api/comment")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly ICommentRepository _commentRepository;
        private readonly IStockRepository _stockRepository;
        public CommentController(ICommentRepository commentRepository, IStockRepository stockRepository)
        {
            _commentRepository = commentRepository;
            _stockRepository = stockRepository;
        }
        [HttpGet]
        public async Task<IActionResult> GetComments()
        {
            var comments = await _commentRepository.GetComments();
            var commentDto = comments.Select(c => c.toCommentDto());
            return Ok(comments);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCommentID(int id)
        {
            var comment = await _commentRepository.GetCommentID(id);
            if (comment == null)
            {
                return NotFound();
            }
            return Ok(comment.toCommentDto());
        }
        [HttpPost("{stockId}")]
        public async Task<IActionResult> CreateComment([FromRoute]int stockId, CreateCommentDto createCommentDto)
        {
            if(! await _stockRepository.StockExists(stockId))
            {
                return BadRequest("Stock does not exist");
            }
            var commentModel = createCommentDto.ToCommentFromCreate(stockId);
            var comment = await _commentRepository.CreateComment(commentModel);
            return CreatedAtAction(nameof(GetCommentID), new { id = comment.Id }, comment.toCommentDto());
        }
        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> UpdateComment([FromRoute] int id, UpdateCommentRequest updateCommentDto)
        {
            var comment = await _commentRepository.UpdateComment(id, updateCommentDto.ToCommentFromUpdate());
            if (comment == null)
            {
                return NotFound();
            }
            return Ok(comment.toCommentDto());
        }
    }
}