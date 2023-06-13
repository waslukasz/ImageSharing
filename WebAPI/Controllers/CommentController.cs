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

        /// <summary>
        /// Add a comment to a specific post
        /// </summary>
        /// <param name="request">Comment details</param>
        /// <returns></returns>
        [HttpPost("Add")]
        [Authorize]
        public async Task<IActionResult> AddComment([FromBody] AddCommentRequest request)
        {
            UserEntity? user = await _userManager.GetUserAsync(HttpContext.User);

            Guid newCommentGuId = await _commentService.AddComment(request, user);
            return Ok(new { CommentGuId = newCommentGuId, Text = request.Text });

        }

        /// <summary>
        /// Delete a specific comment
        /// </summary>
        /// <param name="id">Comment GuId</param>
        /// <returns></returns>

        [HttpDelete("Delete/{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteComment(Guid id)
        {
            UserEntity? user = await _userManager.GetUserAsync(HttpContext.User);
            await _commentService.Delete(id, user);
            
            return NoContent();
        }

        /// <summary>
        /// Edit a specific comment
        /// </summary>
        /// <param name="id">Comment GuId</param>
        /// <param name="request">Comment content</param>
        /// <returns></returns>

        [HttpPatch("Edit/{id}")]
        [Authorize]
        public async Task<IActionResult> EditComment([FromBody] EditCommentRequest request, Guid id)
        {
            UserEntity? user = await _userManager.GetUserAsync(HttpContext.User) ?? throw new UserNotFoundException();
            CommentDto commentDto = await _commentService.Edit(request, user, id);
            
            return Ok(commentDto);
        }

        /// <summary>
        /// Get all comments for a given post
        /// </summary>
        /// <param name="request">Post GuId and response size</param>
        /// <returns></returns>

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAllComments([FromQuery] GetAllCommentsRequest request)
        {
            if (!ModelState.IsValid) 
                return BadRequest();

            PaginatorResult<CommentDto> response = await _commentService.GetAll(request);

            return Ok(response);
        }

        /// <summary>
        /// Get comment by id
        /// </summary>
        /// <param name="id">Comment GuId</param>
        /// <returns></returns>
        [HttpGet("Get/{id}")]
        public async Task<IActionResult> GetCommentById(Guid id)
        {
            CommentDto comment = await _commentService.FindByGuId(id);
            return Ok(comment);
        }
    }
}
