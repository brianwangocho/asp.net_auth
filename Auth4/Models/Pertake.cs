using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Auth4.Models
{
    public class Pertake
    {

        [Key]
        public int Id { get; set; }

        [Required]
        public string UserId { get; set; }

        public string status { get; set; }
        public string name { get; set; }

        public int ActivityId { get; set; }

        [ForeignKey("ActivityId")]
        public Activity activity { get; set; }

        [ForeignKey("UserId")]
        public ApplicationUser user { get; set; }
    }
}