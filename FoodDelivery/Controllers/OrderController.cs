using FoodDelivery.Services;
using Microsoft.AspNetCore.Mvc;
using FoodDelivery.Models.DTO;
using System.Net;
using Microsoft.AspNetCore.Authorization;

namespace FoodDelivery.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;
        private readonly ILogoutService _logoutService;

        public OrderController(IOrderService orderService, ILogoutService logoutService)
        {
            _orderService = orderService;
            _logoutService = logoutService;
        }

        [Authorize]
        [HttpPost]
        public ActionResult<OrderCreateDTO> CreateOrder()
        {
            string token = Request.Headers["Authorization"].ToString().Split(' ')[1];
            if (_logoutService.IsUserLogout(token))
                return Unauthorized(new { status = HttpStatusCode.Unauthorized, message = "User is unauthorized" });

            try
            {
                var orderCreateDTO = _orderService.CreateOrderFromBasket(token);
                if (orderCreateDTO != null)
                    return Ok(orderCreateDTO);
                else
                    return BadRequest(new { status = HttpStatusCode.BadRequest, message = "User cart is empty" });
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [Authorize]
        [HttpGet("{id}")]
        public ActionResult<OrderDTO> GetOrderById(Guid id)
        {
            string token = Request.Headers["Authorization"].ToString().Split(' ')[1];
            if (_logoutService.IsUserLogout(token))
                return Unauthorized(new { status = HttpStatusCode.Unauthorized, message = "User is unauthorized" });

            //validate Guid (?)
            Guid guidOutput = Guid.NewGuid();
            bool isValid = Guid.TryParse(id.ToString(), out guidOutput);
            if (!isValid)
            {
                return BadRequest(new { status = HttpStatusCode.BadRequest, message = "Id is incorrect" });
            }

            try
            {
                var order = _orderService.GetOrderById(id, token);
                if (order == null)
                    return NotFound(new { status = HttpStatusCode.NotFound, message = "Order not found" });
                return Ok(order);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [Authorize]
        [HttpGet]
        public ActionResult<OrderListDTO> GetOrderList()
        {
            string token = Request.Headers["Authorization"].ToString().Split(' ')[1];
            if (_logoutService.IsUserLogout(token))
                return Unauthorized(new { status = HttpStatusCode.Unauthorized, message = "User is unauthorized" });

            try
            {
                var orderlist = _orderService.GetOrderList(token);
                return Ok(orderlist);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [Authorize]
        [HttpPost("{id}/status")]
        public IActionResult ConfirmOrderDelivery(Guid id)
        {
            string token = Request.Headers["Authorization"].ToString().Split(' ')[1];
            if (_logoutService.IsUserLogout(token))
                return Unauthorized(new { status = HttpStatusCode.Unauthorized, message = "User is unauthorized" });

            //validate Guid (?)
            Guid guidOutput = Guid.NewGuid();
            bool isValid = Guid.TryParse(id.ToString(), out guidOutput);
            if(!isValid)
            {
                return BadRequest(new { status = HttpStatusCode.BadRequest, message = "Id is incorrect" });
            }

            try
            {
                string status = _orderService.ConfirmDelivery(token, id);
                if(status == "orders not found")
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
