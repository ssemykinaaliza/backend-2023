using FoodDelivery.Models.Enum;
using System.ComponentModel.DataAnnotations;

namespace FoodDelivery.Models.DTO
{
    public class OrderInfoDTO
    {
        public Guid Id { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime DeliveryTime { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime OrderTime { get; set; }

        [EnumDataType(typeof(OrderStatus), ErrorMessage = "Invalid order status")]
        public OrderStatus Status { get; set; }

        public double Price { get; set; }
    }
}
