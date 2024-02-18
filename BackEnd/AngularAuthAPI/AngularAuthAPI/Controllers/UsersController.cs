using AngularAuthAPI.Context;
using AngularAuthAPI.Helpers;
using AngularAuthAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.IdentityModel.Tokens.Jwt;
using System.Threading.Tasks;
using System.Text;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;

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

          if (PasswordHasher.verifyPassword(userObj.Password, user.Password))
            {
                return BadRequest(new { message = "password is incorrect" });
            }

          userObj.Token = GenerateJwt(user)
        }


        //Generate jwt tokens
        private string GenerateJwt(Users users)
        {
            var jwtTokenHandler = new JwtSecurityTokenHandler();

            //Create key
            var key = Encoding.ASCII.GetBytes("SECRET USED TO VERIFY JWT TOKENS..");

            //Create payLoader
            var identity = new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.Role, users.Role),
                new Claim(ClaimTypes.Name, users.FirstName, users.FirstName)
            });

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = identity,
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = jwtTokenHandler.CreateToken(tokenDescriptor);

            return jwtTokenHandler.WriteToken(token);
        }


    }
}
