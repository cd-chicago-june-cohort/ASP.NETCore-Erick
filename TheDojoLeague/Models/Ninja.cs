using System.ComponentModel.DataAnnotations;
namespace TheDojoLeague.Models
{
    public abstract class BaseEntity {}
    public class Ninja : BaseEntity
    {
        [Key]
        public long Id { get; set; }

        [Required]
        [MinLength(2)]
        [Display(Name = "Ninja Name")]
        public string Name { get; set; }
 
        [Required]
        [MinLength(10)]
        [Display(Name = "Ninja Level(1-10)")]
        public int Level { get; set; }
 
        public int Dojo_Id { get; set; }
        public Dojo dojo { get; set; }

        [MinLength(5)]
        [Display(Name = "Optional Description")]
        public string Description { get; set; }
    }
}
