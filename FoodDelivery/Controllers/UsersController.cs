using FoodDelivery.Models;
using FoodDelivery;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using FoodDelivery.Services;
using FoodDelivery.Models.DTO;
using Microsoft.Extensions.Caching.Distributed;
using System.Text;
using System.Net;
using Microsoft.AspNetCore.Authorization;
using System.Text.RegularExpressions;

namespace FoodDelivery.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUsersService _usersService;
        private readonly ILogoutService _logoutService;

        public UsersController(IUsersService usersService, ILogoutService logoutService)
        {
            _usersService = usersService;
            _logoutService = logoutService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] UserRegisterDTO model)
        {
            if (ModelState.IsValid)
            {
                Regex regex = new Regex(@"[0-9]");
                MatchCollection matches = regex.Matches(model.Password);
                if (matches.Count < 0)
                {
                    return BadRequest(new { status = HttpStatusCode.BadRequest, message = "Password requires at least one digit" });
                }
                try
                {
                    if (_usersService.IsUserUnique(model))
                    {
                        await _usersService.Register(model);
                        return new JsonResult(new
                        {
                            token = _usersService.GetToken(
                            ConverterDTO.Login(model))
                        });
                    }
                    else
                    {
                        return Conflict(new { status = HttpStatusCode.Conflict, message = "User already exists" });
                    }
                }
                catch
                {
                    return StatusCode(StatusCodes.Status500InternalServerError);
                }
            }
            return BadRequest(new { status = HttpStatusCode.BadRequest, message = "Incorrect data" });
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginCredentials userLoginData)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var jwt = _usersService.GetToken(userLoginData);
                    if (jwt != null)
                    {
                        return Ok(new { token = jwt });//new JsonResult(new { token = jwt })
                    }
                    return BadRequest(new { status = HttpStatusCode.BadRequest, message = "Wrong email or password" });
                }
                catch
                {
                    return StatusCode(StatusCodes.Status500InternalServerError);
                }
            }
            return BadRequest(new { status = HttpStatusCode.BadRequest, message = "Incorrect data" });
        }

        [Authorize]
        [HttpGet("profile")]
        public IActionResult GetProfile()
        {
            string token = Request.Headers["Authorization"].ToString().Split(' ')[1];
            if (_logoutService.IsUserLogout(token))
                return Unauthorized(new { status = HttpStatusCode.Unauthorized, message = "User is unauthorized" });
            try
            {
                var user = _usersService.GetUser(token);
                if (user == null)
                    return BadRequest(new { status = HttpStatusCode.BadRequest, message = "User not found" });

                return Ok(user);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [Authorize]
        [HttpPut("profile")]
        public IActionResult EditProfile([FromBody] UserEditDTO model)
        {
            string token = Request.Headers["Authorization"].ToString().Split(' ')[1];
            if (_logoutService.IsUserLogout(token))
                return Unauthorized(new { status = HttpStatusCode.Unauthorized, message = "User is unauthorized" });
            if (ModelState.IsValid)
            {
                try
                {
                    if (_usersService.EditUser(model, token))
                        return Ok();
                }
                catch 
                {
                    return StatusCode(StatusCodes.Status500InternalServerError);
                }
            }
            return BadRequest(new { status = HttpStatusCode.BadRequest, message = "Incorrect data" });
        }

        [Authorize]
        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            string token = Request.Headers["Authorization"].ToString().Split(' ')[1];
            if (_logoutService.IsUserLogout(token))
                return Unauthorized(new { status = HttpStatusCode.Unauthorized, message = "User is unauthorized" });
            try
            {
                await _logoutService.Logout(token);
                return Ok();
            }
            catch 
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
