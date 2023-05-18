using Infrastructure.EF.Entity;
using Infrastructure.Manager;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Request;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CommentController : ControllerBase
    {
        private readonly UserManager<UserEntity> _userManager;

        public CommentController(UserManager<UserEntity> userManager)
        {
            _userManager = userManager;
        }

        [HttpPost]
        public async Task<IActionResult> AddComment([FromBody] AddCommentRequest request)
        {
            UserEntity? user = await _userManager.GetUserAsync(HttpContext.User);

            //TODO NIE ZNAJDUJE USERA
            //if(user is null) return Unauthorized();

            //await _reactionManager.AddReaction(request, user);
            return Ok();

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

        [HttpGet]
        public async Task<IActionResult> GetAllComments()
        {
            throw new NotImplementedException();
        }

        [HttpGet("{CommentId}")]
        public async Task<IActionResult> GetCommentById()
        {
            throw new NotImplementedException();
        }
    }
}
