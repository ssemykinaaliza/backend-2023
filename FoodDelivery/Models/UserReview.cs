using System.Text.Json.Serialization;

namespace FoodDelivery.Models
{
    public class UserReview
    {
        public Guid Id { get; set; }

        public User User { get; set; }

        public int Rating { get; set; }

        public Dish Dish { get; set; }
    }
}
