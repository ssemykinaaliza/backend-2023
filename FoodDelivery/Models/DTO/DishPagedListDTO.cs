namespace FoodDelivery.Models.DTO
{
    public class DishPagedListDTO
    {
        public ICollection<DishDTO>? Dishes { get; set; }

        public PageInfoDTO Pagination { get; set; }
    }
}
