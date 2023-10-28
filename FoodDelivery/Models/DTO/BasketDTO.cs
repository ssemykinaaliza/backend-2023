namespace FoodDelivery.Models.DTO
{
    public class BasketDTO
    {
        public ICollection<DishBasketDTO> Dishes { get; set; }
    }
}
