using FoodDelivery.Models.DTO;
using FoodDelivery.Models.Enum;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FoodDelivery.Models
{
    public class User
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        [MinLength(1, ErrorMessage = "The fullname is too short")]
        public string FullName { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime? BirthDate { get; set; }

        [Required]
        [EnumDataType(typeof(Gender), ErrorMessage = "Invalid gender")]
        public Gender Gender { get; set; }

        public string? Address { get; set; }

        [Required]
        [EmailAddress(ErrorMessage = "Invalid email")]
        public string Email { get; set; }

        [Required]
        [MinLength(6, ErrorMessage = "The password is too short")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [Phone(ErrorMessage = "Invalid phone number")]
        public string? PhoneNumber { get; set; }

        public ICollection<Order> Orders { get; set; } = new List<Order>();

        public ICollection<DishBasket> Cart { get; set; } = new List<DishBasket>();

        /*public User()
        { 
            Orders = new List<Order>();
            Cart = new List<DishBasket>();
        }*/
    }
}
