using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Auth4.Models
{
    public class Rotter
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string PertakerId { get; set; }

    
        public Pertake pertake { get; set; }

        [Required]
        public string UserId { get; set; }

    
        public int TaskId { get; set; }

       

        
        public string status { get; set; }

        public DateTime StartedOn { get; set; }


        public DateTime EndedOn { get; set; }
    }
}