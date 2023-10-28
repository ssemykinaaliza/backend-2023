using System.ComponentModel.DataAnnotations;

namespace FoodDelivery.Models.DTO
{
    public class LoginCredentials
    {
        [Required]
        [MinLength(1, ErrorMessage = "The email is too short.")]
        [EmailAddress (ErrorMessage = "Invalid email")]
        public string Email { get; set; }

        [Required]
        [MinLength(1, ErrorMessage = "The password is too short.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
