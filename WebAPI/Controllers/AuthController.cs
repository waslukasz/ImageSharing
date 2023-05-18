using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Managers.Interfaces;
using WebAPI.Request;

namespace WebAPI.Controllers
{
    [ApiController, Route("/api/auth")]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthManager _authManager;

        public AuthenticationController(IConfiguration configuration, IAuthManager authManager)
        {
            _authManager = authManager;
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Authenticate([FromBody] LoginUserRequest request)
        {
            return Ok(await _authManager.Login(request));
        }
    }
}