using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WeddingPlanner.Models
{
    public class GuestAtWedding : BaseEntity
    {
        public int GuestAtWeddingId { get; set; }
        
        public int GuestId { get; set; }
               
        public User Guest { get; set; }
        
        public int WeddingId { get; set; }
                
        public Wedding Wedding { get; set; }
    }
}