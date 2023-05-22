using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Request;
using WebAPI.Services.Interfaces;

namespace WebAPI.Controllers
{
    [ApiController, Route("/api/auth")]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthenticationController(IConfiguration configuration, IAuthService authService)
        {
            _authService = authService;
        }
        
        /// <summary>
        /// Get JWT Security token for account
        /// </summary>
        /// <param name="request">Account details</param>
        /// <returns></returns>
        [HttpPost("Login")]
        [AllowAnonymous]
        public async Task<IActionResult> Authenticate([FromBody] LoginUserRequest request)
        {
            return Ok(new { token = await _authService.Login(request) });
        }
    }
}