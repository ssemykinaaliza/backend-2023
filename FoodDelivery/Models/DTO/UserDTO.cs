using System.ComponentModel.DataAnnotations;
using FoodDelivery.Models.Enum;
using System.Reflection;

namespace FoodDelivery.Models.DTO
{
    public class UserDTO
    {
        public Guid Id { get; set; }

        [Required]
        [MinLength(1, ErrorMessage = "The fullname is too short.")]
        public string FullName { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime? BirthDate { get; set;}

        [Required]
        [EnumDataType(typeof(Gender), ErrorMessage = "Invalid gender")]
        public Gender Gender { get; set; }

        public string? Address{ get; set; }

        [Required]
        [EmailAddress (ErrorMessage = "Invalid email")]
        public string? Email { get; set; }

        [Required]
        [Phone(ErrorMessage = "Invalid phone number")]
        public string? PhoneNumber { get; set; }
    }
}
