using System.ComponentModel.DataAnnotations;

namespace FormSubmission.Models
{
    public abstract class BaseEntity {}
    public class User : BaseEntity
    {
        [Required]
        [MinLength(4)]
        public string firstName { get; set; }

        [MinLength(4)]
        [Required]
        public string lastName { get; set; }

        [Required]
        [Range(0, 9999)]
        public int age { get; set; }
        
        [Required]
        [EmailAddress]
        public string email { get; set; }

        [Required]
        [MinLength(8)]
        public string password { get; set; }
    }
}