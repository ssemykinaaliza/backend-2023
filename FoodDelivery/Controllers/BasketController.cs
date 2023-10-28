using FoodDelivery.Services;
using Microsoft.AspNetCore.Mvc;
using FoodDelivery.Models.DTO;
using Microsoft.AspNetCore.Authorization;
using System.Net;

namespace FoodDelivery.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BasketController : ControllerBase
    {
        private readonly IBasketService _basketService;
        private readonly ILogoutService _logoutService;

        public BasketController(IBasketService basketService, ILogoutService logoutService)
        {
            _basketService = basketService;
            _logoutService = logoutService;
        }

        [Authorize]
        [HttpGet]
        public ActionResult<BasketDTO> GetUserCart()
        {
            string token = Request.Headers["Authorization"].ToString().Split(' ')[1];
            if (_logoutService.IsUserLogout(token))
                return Unauthorized(new { status = HttpStatusCode.Unauthorized, message = "User is unauthorized" });

            try
            {
                var cart = _basketService.GetUserCart(token);
                if (cart == null)
                    return BadRequest(new { status = HttpStatusCode.BadRequest, message = "Empty cart" });
                else
                    return Ok(cart);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [Authorize]
        [HttpPost("dish/{dishId}")]
        public IActionResult AddDish(Guid dishId)
        {
            string token = Request.Headers["Authorization"].ToString().Split(' ')[1];
            if (_logoutService.IsUserLogout(token))
                return Unauthorized(new { status = HttpStatusCode.Unauthorized, message = "User is unauthorized" });

            try
            {
                string status = _basketService.AddDishToCart(dishId, token);
                if (status == "user is not exists")
                    return BadRequest(new { status = HttpStatusCode.BadRequest, message = status });
                if (status == "dish is not exists")
                    return NotFound(new { status = HttpStatusCode.NotFound, message = status });
                return Ok(new { status = HttpStatusCode.OK, message = status });
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [Authorize]
        [HttpDelete("dish/{dishId}")]
        public IActionResult DeleteDish(Guid dishId, bool increase = false) 
        {
            string token = Request.Headers["Authorization"].ToString().Split(' ')[1];
            if (_logoutService.IsUserLogout(token))
                return Unauthorized(new { status = HttpStatusCode.Unauthorized, message = "User is unauthorized" });
            try
            {
                string status = _basketService.DeleteDishFromCart(dishId, increase, token);
                if (status == "user is not exists")
                    return BadRequest(new { status = HttpStatusCode.BadRequest, message = status });
                if (status == "dish is not found")
                    return NotFound(new { status = HttpStatusCode.NotFound, message = status });
                return Ok(new { status = HttpStatusCode.OK, message = status });
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
