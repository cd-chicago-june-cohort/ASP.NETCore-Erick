using System;
using System.ComponentModel.DataAnnotations;
namespace Restaurant.Models
{
    public abstract class BaseEntity {}
    public class Review : BaseEntity
    {
        [Key]
        public long Id { get; set; }

        [Required]
        [Display(Name = "Reviewer Name: ")]
        public string Author { get; set; }
 
        [Required]
        [Display(Name = "Restaurant Name: ")]
        public string Restaurant { get; set; }
 
        [Required]
        [MinLength(10)]
        [Display(Name = "Review: ")]
        public string Content { get; set; }
 
        [Required]
        [Range(1, 5)]
        [Display(Name = "Stars: ")]
        public int Stars { get; set; }

        [Required]
        [Display(Name = "Date of visit: ")]
        [DataType(DataType.DateTime)]
        public DateTime Date { get; set; }
    }
}