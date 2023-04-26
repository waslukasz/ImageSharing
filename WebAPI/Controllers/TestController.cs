using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
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
            return "You are authorized!";
        }
        
        [HttpGet("auth/admin"), Authorize(Roles = "Admin")]
        public string AdminRole()
        {
            return "You have Admin privileges!";
        }
    }
}
