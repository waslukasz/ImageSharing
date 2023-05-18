using Infrastructure.EF.Entity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Request;
using Infrastructure.Manager;
using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNet.Identity;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReactionController : ControllerBase
    {
        private readonly UserManager<UserEntity> _userManager;
        private readonly ReactionManager _reactionManager;

        public ReactionController(UserManager<UserEntity> userManager, ReactionManager reactionManager)
        {
            _userManager = userManager;
            _reactionManager = reactionManager;
        }
        //TODO: michał zrób plox xoxo
      /*  [HttpPost]
        public async Task<IActionResult> AddReaction([FromBody] AddReactionRequest request)
        {
            UserEntity? user = await _userManager.GetUserAsync(HttpContext.User);
            
            if(user is null) return Unauthorized();

            await _reactionManager.AddReaction(request, user);
            return Ok();
        }*/
    }
}
