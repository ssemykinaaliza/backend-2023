using FoodDelivery.Models.DTO;
using FoodDelivery.Models.Enum;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace FoodDelivery.Models
{
    public class Order
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime DeliveryTime { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime OrderTime { get; set; }

        [EnumDataType(typeof(OrderStatus), ErrorMessage = "Invalid order status")]
        public OrderStatus Status { get; set; }

        [Required]
        [MinLength(1, ErrorMessage = "The address is too short")]
        public string Address { get; set; }

        public double Price { get; set; }

        public ICollection<DishOrder> DishesInOrder { get; set; } = new List<DishOrder>();

        [JsonIgnore] 
        public ICollection<User> Users { get; set; } = new List<User>();
    }
}
