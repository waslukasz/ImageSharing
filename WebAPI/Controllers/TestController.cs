using System.Security.Claims;
using System.Security.Principal;
using System.Text.Json;
using Application_Core.Exception;
using Infrastructure.EF.Entity;
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        private readonly Microsoft.AspNetCore.Identity.UserManager<UserEntity> _userManager;

        public TestController(Microsoft.AspNetCore.Identity.UserManager<UserEntity> userManager)
        {
            _userManager = userManager;
        }
        
        [HttpGet("auth/anonymous")]
        public string Anonymous()
        {
            return "You have access to this field.";
        }
        
        [HttpGet("auth/authorized"), Authorize]
        public string Authorized()
        {
            return $"You are authorized!";
        }
        
        [HttpGet("auth/user"), Authorize(Roles = "User")]
        public string UserRole()
        {
            return "You have User privileges!";
        }
        
        [HttpGet("auth/admin"), Authorize(Roles = "Admin")]
        public string AdminRole()
        {
            return "You have Admin privileges!";
        }

        [HttpGet("GetUser")]
        public async Task<string> GetUser()
        {
            var result = await _userManager.GetUserAsync(HttpContext.User);
            return result is null ? "nul" : result.UserName;
        }
        
        [HttpGet("exception/test")]
        public IActionResult CheckExceptionFilter()
        {
            throw new ImageNotFoundException();
        }
    }
}
