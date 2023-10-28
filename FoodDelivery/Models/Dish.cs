using FoodDelivery.Models.Enum;
using System.ComponentModel.DataAnnotations;
using FoodDelivery.Models.DTO;
using System.Text.Json.Serialization;

namespace FoodDelivery.Models
{
    public class Dish
    {
        public Guid Id { get; set; }

        [Required]
        [MinLength(1, ErrorMessage = "The name is too short")]
        public string Name { get; set; }

        public string? Description { get; set; }

        public double Price { get; set; }

        public string? Image { get; set; }

        public bool Vegetarian { get; set; } = false;

        public double Rating { get; set; } = 0;

        [EnumDataType(typeof(DishCategory), ErrorMessage = "Invalid dish category")]
        public DishCategory DishCategory { get; set; }

        //public ICollection<UserReview> UserReviews { get; set; } = new List<UserReview>();
    }
}
