using System.ComponentModel.DataAnnotations.Schema;

namespace FoodDelivery.Models
{
    [Table("as_addr_obj")]
    public class Address
    {
        public int id { get; set; }
        public int objectid { get; set; }
        public Guid objectguid { get; set; }
        public int changeid { get; set; }
        public string name { get; set; }
        public string typename { get; set; }
        public string level { get; set; }
        public int opertypeid { get; set; }
        public int previd { get; set; }
        public int nextid { get; set; }
        public DateTime updatedate { get; set; }
        public DateTime startdate { get; set; }
        public DateTime enddate { get; set; }
        public int isactual { get; set; }
        public int isactive { get; set; }
    }
}
