using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace T1_PR2_Client.Models
{
    public class RegisterUser
    {
        [Required]
        [DisplayName("Name and surnames")]
        public string Name { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DisplayName("Username")]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [MinLength(8, ErrorMessage = "Password must be at least 8 characters long.")]
        [MaxLength(100, ErrorMessage = "Password cannot exceed 100 characters.")]
        [RegularExpression(
            @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[\W_]).+$",
            ErrorMessage = "Password must contain at least one uppercase letter, one lowercase letter, one number, and one special character."
        )]
        public string Password { get; set; }


        [DataType(DataType.Password)]
        [DisplayName("Confirm password")]
        [Compare("Password", ErrorMessage = "The password and the confirmation password must be the same!")]
        public string ConfirmationPassword { get; set; }
    }
}
