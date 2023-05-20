using Infrastructure.EF.Entity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Request;
using WebAPI.Services.Interfaces;


namespace WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReactionController : ControllerBase
    {
        private readonly UserManager<UserEntity> _userManager;
        private readonly IReactionService _reactionService;

        public ReactionController(UserManager<UserEntity> userManager, IReactionService reactionService)
        {
            _userManager = userManager;
            _reactionService = reactionService;
        }
        
        [HttpPost("Toggle")]
        public async Task<IActionResult> AddReaction([FromBody] AddReactionRequest request)
        {
            UserEntity? user = await _userManager.GetUserAsync(HttpContext.User);
             
            if(user is null) 
                return Unauthorized();

            await _reactionService.ToggleReaction(request, user);
            return Ok();
        }
    }
}
