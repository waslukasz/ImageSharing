using Application_Core.Model;
using Infrastructure.Dto;
using Infrastructure.EF.Entity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Request;
using WebAPI.Services;
using WebAPI.Services.Interfaces;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CommentController : ControllerBase
    {
        private readonly UserManager<UserEntity> _userManager;
        private readonly ICommentService _commentService;
        

        public CommentController(UserManager<UserEntity> userManager, ICommentService commentService)
        {
            _userManager = userManager;
            _commentService = commentService;
        }
        [HttpPost("Add")]
        public async Task<IActionResult> AddComment([FromBody] AddCommentRequest request)
        {
            UserEntity? user = await _userManager.GetUserAsync(HttpContext.User);

            if (user is null) return Unauthorized();

            Guid newCommentGuId = await _commentService.AddComment(request, user);
            return Ok($"Comment GuId: {newCommentGuId}");

        }

        //TODO: admin i własciciel moze usuwac
        [HttpDelete("Delete/{CommentGuId}")]
        public async Task<IActionResult> DeleteComment([FromRoute] Guid CommentGuId)
        {
            await _commentService.Delete(CommentGuId);
            return NoContent();
        }
        //TODO: własciciel moze edytowac 

        [HttpPatch("Edit")]
        public async Task<IActionResult> EditComment([FromBody] EditCommentRequest request)
        {
            CommentDto commentDto = await _commentService.Edit(request);
            return Ok(commentDto);
        }
  
        //TODO: paginacja
        [HttpGet("GetAll/{PostGuId}")]
        public async Task<IActionResult> GetAllComments([FromRoute] Guid PostGuId)
        {
            return Ok(await _commentService.GetAll(PostGuId));
        }

        [HttpGet("GetById/{CommentGuId}")]
        public async Task<IActionResult> GetCommentById([FromRoute] Guid CommentGuId)
        {
            CommentDto comment = await _commentService.FindByGuId(CommentGuId);
            return Ok(comment);
        }
    }
}
