using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WeddingPlanner.Models;

namespace WeddingPlanner.Models
{
    public class WeddingViewModel : BaseEntity
    {
        [Required]
        public string WedderOne { get; set; }
        
        [Required]
        public string WedderTwo { get; set; }
        
        [Required]        
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }
        
        [Required]
        public string Address { get; set; }
    }
}