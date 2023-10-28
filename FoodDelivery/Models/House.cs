using System.ComponentModel.DataAnnotations.Schema;

namespace FoodDelivery.Models
{
    [Table("as_houses")]
    public class House
    {
        public int id { get; set; }
        public int objectid { get; set; }
        public Guid objectguid { get; set; }
        public int changeid { get; set; }
        public string? housenum { get; set; }
        public string? addnum1 { get; set; }
        public string? addnum2 { get; set; }
        public int? housetype { get; set; }
        public int? addtype1 { get; set; }
        public int? addtype2 { get; set; }
        public int? opertypeid { get; set; }
        public int? previd { get; set; }
        public int? nextid { get; set; }
        public DateTime updatedate { get; set; }
        public DateTime startdate { get; set; }
        public DateTime enddate { get; set; }
        public int isactual { get; set; }
        public int isactive { get; set; }
    }
}
