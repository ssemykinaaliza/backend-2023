using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace FoodDelivery.Models
{
    public class DishBasket
    {
        public Guid Id { get; set; }
        public string IdOfDish { get; set; }

        [Required]
        [MinLength(1, ErrorMessage = "The name is too short.")]
        public string Name { get; set; }

        public double Price { get; set; }

        public double TotalPrice { get; set; }

        public int Amount { get; set; }

        public string? Image { get; set; }

        [JsonIgnore]
        public ICollection<User> Users { get; set; } = new List<User>();
    }
}
