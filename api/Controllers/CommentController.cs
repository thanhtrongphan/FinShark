using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Comment;
using api.Interfraces;
using api.Mappers;
using api.Repository;
using Microsoft.AspNetCore.Authorization;
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
        [Authorize]
        public async Task<IActionResult> GetComments()
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var comments = await _commentRepository.GetComments();
            var commentDto = comments.Select(c => c.toCommentDto());
            return Ok(comments);
        }
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetCommentID(int id)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var comment = await _commentRepository.GetCommentID(id);
            if (comment == null)
            {
                return NotFound();
            }
            return Ok(comment.toCommentDto());
        }
        [HttpPost("{stockId:int}")]
        public async Task<IActionResult> CreateComment([FromRoute]int stockId, CreateCommentDto createCommentDto)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if(! await _stockRepository.StockExists(stockId))
            {
                return BadRequest("Stock does not exist");
            }
            var commentModel = createCommentDto.ToCommentFromCreate(stockId);
            var comment = await _commentRepository.CreateComment(commentModel);
            return CreatedAtAction(nameof(GetCommentID), new { id = comment.Id }, comment.toCommentDto());
        }
        [HttpPut]
        [Route("{id:int}")]
        public async Task<IActionResult> UpdateComment([FromRoute] int id, UpdateCommentRequest updateCommentDto)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var comment = await _commentRepository.UpdateComment(id, updateCommentDto.ToCommentFromUpdate());
            if (comment == null)
            {
                return NotFound();
            }
            return Ok(comment.toCommentDto());
        }
        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IActionResult> DeleteComment([FromRoute] int id)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var comment = await _commentRepository.DeleteComment(id);
            if (comment == null)
            {
                return NotFound("Comment not doesn't exist");
            }
            return Ok(comment.toCommentDto());
        }
    }
}