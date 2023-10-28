namespace FoodDelivery.Models.DTO
{
    public class AddressPagedDTO
    {
        public IEnumerable<AddressDTO>? Addresses { get; set; }

        public PageInfoDTO Pagination { get; set; }
    }
}
