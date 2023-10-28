namespace FoodDelivery.Models.DTO
{
    public class HousesDTO
    {
        public int id { get; set; }
        public int objectid { get; set; }
        public Guid objectguid { get; set; }
        public string? housenum { get; set; }
        public string? addnum1 { get; set; }
        public string? addnum2 { get; set; }
        public int? housetype { get; set; }
        public int? addtype1 { get; set; }
        public int? addtype2 { get; set; }
        public int? opertypeid { get; set; }
    }
}
