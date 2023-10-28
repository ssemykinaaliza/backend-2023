namespace FoodDelivery.Models.DTO
{
    public class OrderListDTO
    {
        public ICollection<OrderInfoDTO> OrderList { get; set; } = new List<OrderInfoDTO>();
    }
}
