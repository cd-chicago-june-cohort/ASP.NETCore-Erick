using System.ComponentModel.DataAnnotations;

namespace LoginReg.Models
{
    public abstract class BaseEntity {}
    public class User : BaseEntity
    {
        [Required]
        [MinLength(2)]
        [Display(Name = "First Name")]
        public string firstName { get; set; }

        [MinLength(2)]
        [Required]
        [Display(Name = "Last Name")]
        public string lastName { get; set; }
        
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string email { get; set; }

        [Required]
        [MinLength(8)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string password { get; set; }

        [Required]
        [MinLength(8)]
        [DataType(DataType.Password)]
        [Compare("password", ErrorMessage="Both passwords must match boi!")]
        [Display(Name = "Password Confirmation")]
        public string passwordConfirmation { get; set; }
    }
}