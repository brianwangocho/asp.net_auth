using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Auth4.Models
{
    [Table("Files")]
    public class Files
    {
        [Key]
        public string Id { get; set; }

        [Required]
        public string DeptClassId { get; set; }

        public string FileName { get; set; }

        [Required]
     
        public DateTime CreatedOn { get; set; }
        [Required]
        public string CreatedBy { get; set; }

        public DateTime ModifiedOn { get; set; }

        public virtual DeptClass DeptClass { get; set; }
    }
}