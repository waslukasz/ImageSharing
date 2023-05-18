using Infrastructure.EF.Entity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Request;
using WebAPI.Services;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CommentController : ControllerBase
    {
        private readonly UserManager<UserEntity> _userManager;
        private readonly CommentService _commentService;

        public CommentController(UserManager<UserEntity> userManager, CommentService commentService)
        {
            _userManager = userManager;
            _commentService = commentService;
        }

        [HttpPost]
        public async Task<IActionResult> AddComment([FromBody] AddCommentRequest request)
        {
            UserEntity? user = await _userManager.GetUserAsync(HttpContext.User);

            if(user is null) return Unauthorized();

            Guid newCommentGuId = await _commentService.AddComment(request, user);
            return Ok($"Comment GuId: {newCommentGuId}");

        }
        [HttpDelete]
        public async Task<IActionResult> DeleteComment()
        {
            throw new NotImplementedException();
        }

        [HttpPatch]
        public async Task<IActionResult> EditComment()
        {
            throw new NotImplementedException();
        }

        [HttpGet("{PostGuId}")]
        public async Task<IActionResult> GetAllComments([FromRoute] Guid PostGuId)
        {
            return Ok(_commentService.GetAll(PostGuId));
        }

/*        [HttpGet("{CommentId}")]
        public async Task<IActionResult> GetCommentById()
        {
            throw new NotImplementedException();
        }*/
    }
}
