using Application_Core.Model;
using Infrastructure.Dto;
using Infrastructure.EF.Entity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Data;
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
        [Authorize]
        public async Task<IActionResult> AddComment([FromBody] AddCommentRequest request)
        {
            UserEntity? user = await _userManager.GetUserAsync(HttpContext.User);

            Guid newCommentGuId = await _commentService.AddComment(request, user);
            return Ok(new { CommentGuId = newCommentGuId, Text = request.Text });

        }

        [HttpDelete("Delete/{CommentGuId}")]
        [Authorize]
        public async Task<IActionResult> DeleteComment([FromRoute] Guid CommentGuId)
        {
            UserEntity? user = await _userManager.GetUserAsync(HttpContext.User);

            await _commentService.Delete(CommentGuId, user);
            return NoContent();
        }

        [HttpPatch("Edit")]
        [Authorize]
        public async Task<IActionResult> EditComment([FromBody] EditCommentRequest request)
        {
            UserEntity? user = await _userManager.GetUserAsync(HttpContext.User);

            CommentDto commentDto = await _commentService.Edit(request, user);
            return Ok(commentDto);
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAllComments([FromQuery] GetAllCommentsRequest request)
        {
            if (!ModelState.IsValid) return BadRequest();
            return Ok(await _commentService.GetAll(request));
        }

        [HttpGet("GetByGuId/{CommentGuId}")]
        public async Task<IActionResult> GetCommentById([FromRoute] Guid CommentGuId)
        {
            CommentDto comment = await _commentService.FindByGuId(CommentGuId);
            return Ok(comment);
        }
    }
}
