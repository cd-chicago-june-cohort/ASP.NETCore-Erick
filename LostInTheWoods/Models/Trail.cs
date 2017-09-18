using System.ComponentModel.DataAnnotations;
namespace LostInTheWoods.Models
{
    public abstract class BaseEntity {}
    public class Trail : BaseEntity
    {
        [Key]
        public long Id { get; set; }

        [Required]
        [MinLength(2)]
        [Display(Name = "Trail Name: ")]
        public string Name { get; set; }
 
        [Required]
        [MinLength(10)]
        [Display(Name = "Description: ")]
        public string Description { get; set; }
 
        [Required]
        [Display(Name = "Trail Length: ")]
        public int Length { get; set; }
 
        [Required]
        [Display(Name = "Elevation Change: ")]
        public int Elevation_Change { get; set; }

        [Required]
        [Display(Name = "Longitude: ")]
        public float Longitude { get; set; }

        [Required]
        [Display(Name = "Latitude: ")]
        public float Latitude { get; set; }
    }
}