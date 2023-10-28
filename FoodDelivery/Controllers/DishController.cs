using FoodDelivery.Models.DTO;
using FoodDelivery.Models.Enum;
using FoodDelivery.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;
using System.Net;

namespace FoodDelivery.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DishController : ControllerBase
    {
        private readonly IDishService _dishService;
        private readonly ILogoutService _logoutService;

        public DishController(IDishService dishService, ILogoutService logoutService)
        {
            _dishService = dishService;
            _logoutService = logoutService;
        }

        [HttpGet]
        public ActionResult<DishPagedListDTO> GetDishes([FromQuery] DishCategory []categories, [FromQuery] bool vegetarian, 
            [FromQuery] DishSorting sorting, [FromQuery] int page)
        {
            if (page <= 0)
                return BadRequest(new { status = HttpStatusCode.BadRequest, message = "Incorrect page number" });
            try
            {
                DishPagedListDTO? pageList = _dishService.GetDishPagedList(categories, vegetarian, sorting, page);
                if (pageList == null)
                    return BadRequest(new { status = HttpStatusCode.BadRequest, message = "Incorrect page number" });

                return Ok(pageList);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet("{id}")]
        public ActionResult<DishDTO> GetDish(Guid id)
        {
            try
            {
                DishDTO? dish = _dishService.GetDish(id);
                if(dish == null)
                    return NotFound(new { status = HttpStatusCode.NotFound, message = "Dish not found" });

                return Ok(dish);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [Authorize]
        [HttpPost("{id}/rating/check")]
        public ActionResult<bool> Check(Guid id)
        {
            string token = Request.Headers["Authorization"].ToString().Split(' ')[1];
            if (_logoutService.IsUserLogout(token))
                return Unauthorized(new { status = HttpStatusCode.Unauthorized, message = "User is unauthorized" });

            try
            {
                string check = _dishService.Check(id, token);
                if (check == "dish is not found")
                    return NotFound(new { status = HttpStatusCode.NotFound, message = check });
                if (check == "true")
                    return Ok(true);
                return Ok(false);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [Authorize]
        [HttpPost("{id}/rating")]
        public IActionResult SetRating(Guid id, int ratingScore) 
        {
            string token = Request.Headers["Authorization"].ToString().Split(' ')[1];
            if (_logoutService.IsUserLogout(token))
                return Unauthorized(new { status = HttpStatusCode.Unauthorized, message = "User is unauthorized" });

            if(ratingScore < 0 || ratingScore > 10)
                return BadRequest(new { status = HttpStatusCode.BadRequest, message = "Incorrect rating score" });

            try
            {
                string status = _dishService.Set(id, token, ratingScore);

                if (status == "the rating is set" || status == "rating changed")
                    return Ok(new { status = HttpStatusCode.OK, message = status });
                if (status == "user not found" || status == "dish not found")
                    return NotFound(new { status = HttpStatusCode.NotFound, message = status });
                return BadRequest(new { status = HttpStatusCode.BadRequest, message = "Error" });
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
