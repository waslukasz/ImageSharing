using Application_Core.Exception;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
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

        [HttpGet("exception/test")]
        public IActionResult CheckExceptionFilter()
        {
            throw new ImageNotFoundException();
        }
    }
}
