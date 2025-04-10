using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace T1_PR2_Client.Models
{
    public class User
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
        [StringLength(100, ErrorMessage = "The password must have minimum 8 characters", MinimumLength = 8)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [DisplayName("Confirm password")]
        [Compare("Password", ErrorMessage = "The password and the confirmation password must be the same!")]
        public string ConfirmationPassword { get; set; }
    }
}
