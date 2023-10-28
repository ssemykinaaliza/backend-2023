using System.ComponentModel.DataAnnotations.Schema;

namespace FoodDelivery.Models
{
    [Table("as_adm_hierarchy")]
    public class Hierarchy
    {
        public int id { get; set; }
        public int objectid { get; set; }
        public int parentobjid { get; set; }
        public int changeid { get; set; }
        public string regioncode { get; set; }
        public string areacode { get; set; }
        public string citycode { get; set; }
        public string placecode { get; set; }
        public string streetcode { get; set; }
        public int previd { get; set; }
        public int nextid { get; set; }
        public DateTime updatedate { get; set; }
        public DateTime startdate { get; set; }
        public DateTime enddate { get; set; }
        public int isactive { get; set; }
        public string path { get; set; }
    }
}