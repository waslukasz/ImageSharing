using Infrastructure.EF.Entity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Request;
using Infrastructure.Manager;
using WebAPI.Managers;


namespace WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReactionController : ControllerBase
    {
        private readonly UserManager<UserEntity> _userManager;
        private readonly ReactionService _reactionService;

        public ReactionController(UserManager<UserEntity> userManager, ReactionService reactionService)
        {
            _userManager = userManager;
            _reactionService = reactionService;
        }
        
        [HttpPost("add")]
        public async Task<IActionResult> AddReaction([FromBody] AddReactionRequest request)
        {
            UserEntity? user = await _userManager.GetUserAsync(HttpContext.User);
             
            if(user is null) 
                return Unauthorized();

            await _reactionService.AddReaction(request, user);
            return Ok();
        }
    }
}
