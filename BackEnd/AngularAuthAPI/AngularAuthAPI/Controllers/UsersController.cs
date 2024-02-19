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
using System.Text.RegularExpressions;

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

            userObj.Token = GenerateJwt(user);

            return Ok(new
            {
                Token = user.Token,
                Message = "Login Success"
            });
        }

        //method to check if username exist
        private Task<bool> CheckUserNameExistAsync(string username) =>
            _authContext.User.AnyAsync(x => x.Username == username);


        //method to check if email exist
        private Task<bool> CheckUserEmailExistAsync(string email) =>
            _authContext.User.AnyAsync(x => x.Username == email);

        
        //method to check password strength
        private string CheckPasswordStrength(string password)
        {
            StringBuilder sb = new StringBuilder();
            if (password.Length < 9)
                sb.Append("Minimum password should be length be 8" + Environment.NewLine);

            if (!(Regex.IsMatch(password, "[a-z]") && Regex.IsMatch(password, "[A-Z]") && Regex.IsMatch(password, "[0-9]")))  
                sb.Append("Password should be alphanumeric" + Environment.NewLine);

            if (!Regex.IsMatch(password, "[<,>,@,!,#,$,%,^,&,*,(,),_,+,\\[,\\],{,},?,:,;,|,',\\,.,/,~,`,-,=]"))
                sb.Append("Password should contain special characters" + Environment.NewLine);

            return sb.ToString();
        }


        [HttpPost("Register")]
        public async Task<IActionResult> RegisterUser([FromBody] Users userObj)
        {
            if (userObj == null)
                return BadRequest();


            //Check the username
            if (await CheckUserNameExistAsync(userObj.Username))
                return BadRequest(new { Message = "Username Already Exists" });

            //check the password strength
            var passwd = CheckPasswordStrength(userObj.Password);
            if (string.IsNullOrEmpty(passwd))
                return BadRequest(new { message = passwd.ToString() });

            userObj.Password = PasswordHasher.HashPassword(userObj.Password);
            userObj.Role = "User";
            userObj.Token = "";
            await _authContext.User.AddAsync(userObj);
            await _authContext.SaveChangesAsync();

            return Ok(new
            {
                Message = "User registered"
            });
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
