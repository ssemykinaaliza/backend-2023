using System.ComponentModel.DataAnnotations;

namespace FoodDelivery.Models.DTO
{
    public class DishBasketDTO
    {
        public string Id { get; set; }

        [Required]
        [MinLength(1, ErrorMessage = "The name is too short.")]
        public string Name { get; set; }

        public double Price { get; set; }

        public double TotalPrice { get; set; }

        public int Amount { get; set; }

        public string? Image { get; set; }
    }
}
