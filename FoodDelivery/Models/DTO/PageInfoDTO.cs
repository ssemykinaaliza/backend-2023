using System.ComponentModel.DataAnnotations;

namespace FoodDelivery.Models.DTO
{
    public class PageInfoDTO
    {
        [Required]
        [Range(1, Int32.MaxValue)]
        public int Size { get; set; }

        [Required]
        [Range(1, Int32.MaxValue)]
        public int Count { get; set; }

        [Required]
        [Range(1, Int32.MaxValue)]
        public int Current { get; set; }
    }
}
