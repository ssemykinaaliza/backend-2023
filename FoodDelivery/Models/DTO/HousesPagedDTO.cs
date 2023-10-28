namespace FoodDelivery.Models.DTO
{
    public class HousesPagedDTO
    {
        public IEnumerable<HousesDTO>? Houses { get; set; }

        public PageInfoDTO Pagination { get; set; }
    }
}
