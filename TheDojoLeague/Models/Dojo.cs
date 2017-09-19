using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace TheDojoLeague.Models
{
    public class Dojo : BaseEntity
    {
        public Dojo() {
            ninjas = new List<Ninja>();
        }

        [Key]
        public long Id { get; set; }

        [Required]
        [MinLength(2)]
        [Display(Name = "Dojo Name")]
        public string Name { get; set; }

        [Required]
        [MinLength(2)]
        [Display(Name = "Dojo Location")]
        public string Location { get; set; }

        [MinLength(5)]
        [Display(Name = "Additional Dojo Information")]
        public string Information { get; set; }

        public ICollection<Ninja> ninjas { get; set; }
    }
}