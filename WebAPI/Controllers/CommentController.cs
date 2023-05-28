using Infrastructure.Dto;
using Infrastructure.EF.Entity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Application_Core.Exception;
using Infrastructure.EF.Pagination;
using WebAPI.Request;
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

        [HttpDelete("Delete/{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteComment(Guid id)
        {
            UserEntity? user = await _userManager.GetUserAsync(HttpContext.User);
            await _commentService.Delete(id, user);
            
            return NoContent();
        }

        [HttpPatch("Edit/{id}")]
        [Authorize]
        public async Task<IActionResult> EditComment([FromBody] EditCommentRequest request, Guid id)
        {
            UserEntity? user = await _userManager.GetUserAsync(HttpContext.User) ?? throw new UserNotFoundException();
            CommentDto commentDto = await _commentService.Edit(request, user, id);
            
            return Ok(commentDto);
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAllComments([FromQuery] GetAllCommentsRequest request)
        {
            if (!ModelState.IsValid) 
                return BadRequest();

            PaginatorResult<CommentDto> response = await _commentService.GetAll(request);

            return Ok(response);
        }

        [HttpGet("Get/{id}")]
        public async Task<IActionResult> GetCommentById(Guid id)
        {
            CommentDto comment = await _commentService.FindByGuId(id);
            return Ok(comment);
        }
    }
}
