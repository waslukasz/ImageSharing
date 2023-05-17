using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Infrastructure.EF.Entity;
using JWT.Algorithms;
using JWT.Builder;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Configuration;
using WebAPI.Managers;
using WebAPI.Managers.Interfaces;
using WebAPI.Request;

namespace WebAPI.Controllers
{
    [ApiController, Route("/api/auth")]
    public class AuthenticationController : ControllerBase
    {
        private readonly UserManager<UserEntity> _userManager;
        private readonly ILogger _logger;
        private readonly IAuthManager _authManager;

        public AuthenticationController(UserManager<UserEntity> manager, ILogger<AuthenticationController> logger,
            IConfiguration configuration, IAuthManager authManager)
        {
            _userManager = manager;
            _logger = logger;
            _authManager = authManager;
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Authenticate([FromBody] LoginUserRequest request)
        {
            return Ok(await _authManager.Authenticate(request));
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromForm] RegisterUserRequest request)
        {
            return await _authManager.RegisterUser(request) ? NoContent() : BadRequest();
        }
    }
}