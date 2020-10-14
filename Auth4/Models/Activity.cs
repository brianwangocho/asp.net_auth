using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Auth4.Models
{
    public class Activity
    {
        [Key]
        public int Id { get; set; }

        public string Name{ get; set; }

        public string Status { get; set; }


        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; }



        public virtual ICollection<Tasks> Tasks { get; set; }

    }
}