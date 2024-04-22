using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TaskManager_JWT.Models;
using TaskManager_JWT.Services;

namespace TaskManager_JWT.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUser _user;
        private readonly IConfiguration _config;

        public UserController(IUser user, IConfiguration config)
        {
            _user = user;
            _config = config;
        }

        //[HttpPost("createUser")]
        //public IActionResult createUser(User user)
        //{
            
        //    if(user == null)
        //    {
        //        return BadRequest("invalid data");
        //    }
        //    _user.createUser(user);
        //    return Ok("created successFully");
        //}
        



        [HttpGet("getAll")]
        [Authorize(Roles ="admin")]
        public IActionResult GetAll()
        {
            try
            {
                return Ok(_user.getAllUser());
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Internal Server Error: {ex.Message}");
            }
        }

        [HttpPost("login")]
        public IActionResult login([FromBody] Login login)
        {
            try
            {
                if (login == null)
                {
                    return BadRequest("Invalid Username or Password");
                }

                var checkUser =  _user.LogIn(login);
                if (checkUser == null)
                {
                    return BadRequest("Invalid Username or Password");
                }

                var token = GenerateToken(checkUser);
                return Ok(new { Token = token });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Internal Server Error: {ex.Message}");
            }
        }

        private string GenerateToken(User user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.Role, user.Role),
            };

            var token = new JwtSecurityToken(
                claims: claims,
                signingCredentials: credentials,
                expires: DateTime.UtcNow.AddDays(1)
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
