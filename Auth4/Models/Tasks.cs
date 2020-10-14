using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Auth4.Models
{
    public class Tasks
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int? ActvityId { get; set; }

        public Activity activity { get; set; }

        public string Name { get; set; }

        public string Status { get; set; }

        public int  Priority { get; set; }

        public virtual ICollection<Rotter> rotter { get; set; }
    }
}