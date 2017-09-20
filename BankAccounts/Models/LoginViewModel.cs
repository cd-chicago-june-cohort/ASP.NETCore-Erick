using System.ComponentModel.DataAnnotations;
namespace BankAccounts.Models
{
    public class LoginViewModel : BaseEntity
    { 
        [Required]
        [EmailAddress]
        [Display(Name = "Email Address: ")]
        public string Email { get; set; }
 
        [Required]
        [MinLength(6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password: ")]
        public string Password { get; set; }
    }
}