using AngularAuthAPI.Context;
using AngularAuthAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AngularAuthAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly AppDbContext _authContext;

        public UsersController(AppDbContext appDbContext)
        {
            _authContext = appDbContext;
        }


        [HttpPost("Login")]

        public async Task<IActionResult> Authenticate([FromBody] Users userObj)
        {
            if (userObj == null)
                return BadRequest();

            var user = await _authContext.User.FirstOrDefaultAsync(x => x.Username == userObj.Username);

            if (user == null)
                return NotFound(new { Message = "User not found" });

          
        }


    }
}
