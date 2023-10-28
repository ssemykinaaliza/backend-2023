using FoodDelivery.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace FoodDelivery.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AddressController : ControllerBase
    {
        private readonly IAddressService _addressService;

        public AddressController(IAddressService addressService)
        {
            _addressService = addressService;
        }

        /*[HttpGet("/getAddress")]
        public IActionResult getAddress()
        {
            int status = _addressService.getAddress();
            return Ok(new { status = HttpStatusCode.OK, message = status });
        }*/

        [HttpGet("getAddresses")]
        public IActionResult GetAddresses(int page = 1, [FromQuery] string? search = "")
        {
            int pageSize = 50; // Number of addresses per page
            var addresses = _addressService.GetAddresses(page, pageSize, search);

            if (addresses == null)
            {
                return NotFound();
            }

            return Ok(addresses);
        }

        [HttpGet("getHousesByParent")]
        public IActionResult GetHousesByParentObjectId(int parentObjectId, int page = 1)
        {
            int pageSize = 50;
            var houseIds = _addressService.GetHierarchyObjectIds(parentObjectId);

            if (houseIds == null || !houseIds.Any())
            {
                return NotFound();
            }

            var houses = _addressService.GetHousesByObjectIds(houseIds, page, pageSize);

            return Ok(houses);
        }

    }
}
