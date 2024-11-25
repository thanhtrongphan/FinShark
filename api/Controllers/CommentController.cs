using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Comment;
using api.Extensions;
using api.Helpers;
using api.Interfraces;
using api.Mappers;
using api.Models;
using api.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [Route("api/comment")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        // connect to the repository
        private readonly ICommentRepository _commentRepository;
        private readonly IStockRepository _stockRepository;
        private readonly UserManager<AppUser> _userManager;
        private readonly IFMPService _fmpService;
        public CommentController(ICommentRepository commentRepository, IStockRepository stockRepository,
        UserManager<AppUser> userManager, IFMPService fmpService)
        {
            _commentRepository = commentRepository;
            _stockRepository = stockRepository;
            _userManager = userManager;
            _fmpService = fmpService;
        }
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetComments([FromQuery]CommentQueryObject queryObject)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var comments = await _commentRepository.GetComments(queryObject);
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
        [HttpPost]
        [Route("{symbol:alpha}")]
        public async Task<IActionResult> CreateComment([FromRoute] string symbol, CreateCommentDto createCommentDto)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var stock = await _stockRepository.GetStockBySymbol(symbol);
            if (stock == null)
            {
                stock = await _fmpService.FindStockBySymbol(symbol);
                if (stock == null)
                {
                    return NotFound("Stock not found");
                }
                else{
                    stock = await _stockRepository.AddStock(stock);
                }
            }

            var username = User.GetUserName();
            var appUser = await _userManager.FindByNameAsync(username);


            var commentModel = createCommentDto.ToCommentFromCreate(stock.Id);
            commentModel.AppUserId = appUser.Id;
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