using FoodDelivery.Models.Enum;
using System.ComponentModel.DataAnnotations;

namespace FoodDelivery.Models.DTO
{
    public class UserRegisterDTO
    {
        [Required]
        [MinLength(1, ErrorMessage = "The fullname is too short.")]
        public string FullName { get; set; }

        [Required]
        [MinLength(6, ErrorMessage = "The password is too short.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [MinLength(1, ErrorMessage = "The email is too short.")]
        [EmailAddress(ErrorMessage = "Invalid email")]
        public string Email { get; set; }

        public string? Address { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime? BirthDate { get; set; }

        [Required]
        [EnumDataType(typeof(Gender), ErrorMessage = "Invalid gender")]
        public Gender Gender { get; set; }

        [Required]
        [Phone(ErrorMessage = "Invalid phone number")]
        public string? PhoneNumber { get; set; }
    }
}
