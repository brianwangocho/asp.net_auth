using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Auth4.Models
{
    public class Tasks
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int ActvityId { get; set; }


        [ForeignKey("ActvityId ")]
        public virtual Activity activity { get; set; }

        public string Name { get; set; }

        public string AssignTo { get; set; }

        public string Status { get; set; }

        public int  Priority { get; set; }


     

        [ForeignKey("AssignTo")]
        public virtual ApplicationUser user { get; set; }
    }
}