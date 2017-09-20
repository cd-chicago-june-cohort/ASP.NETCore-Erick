using System.ComponentModel.DataAnnotations;
namespace BankAccounts.Models
{
    public class RegisterViewModel : BaseEntity
    {
        [Required]
        [MinLength(2)]
        [Display(Name = "First Name: ")]
        [RegularExpression(@"^[a-zA-Z]+$")]
        public string FirstName { get; set; }

        [Required]
        [MinLength(2)]
        [Display(Name = "Last Name: ")]
        [RegularExpression(@"^[a-zA-Z]+$")]
        public string LastName { get; set; }
 
        [Required]
        [EmailAddress]
        [Display(Name = "Email Address: ")]
        public string Email { get; set; }
 
        [Required]
        [MinLength(6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password: ")]
        public string Password { get; set; }
 
        [Display(Name = "Confirm Password: ")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Password and confirmation must match.")]
        public string PasswordConfirmation { get; set; }
    }
}
