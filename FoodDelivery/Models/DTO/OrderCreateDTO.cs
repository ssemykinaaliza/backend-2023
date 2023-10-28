using System.ComponentModel.DataAnnotations;

namespace FoodDelivery.Models.DTO
{
    public class OrderCreateDTO
    {
        [Required]
        [DataType(DataType.DateTime)]
        public DateTime DeliveryTime { get; set; }

        [Required]
        [MinLength(1, ErrorMessage = "The address is too short.")]
        public string Address { get; set; }
    }
}
