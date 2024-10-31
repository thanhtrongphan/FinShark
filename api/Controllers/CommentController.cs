using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
        public CommentController(ICommentRepository commentRepository)
        {
            _commentRepository = commentRepository;
        }
        [HttpGet]
        public async Task<IActionResult> GetComments()
        {
            var comments = await _commentRepository.GetComments();
            var commentDto = comments.Select(c => c.toCommentDto());
            return Ok(comments);
        }
    }
}