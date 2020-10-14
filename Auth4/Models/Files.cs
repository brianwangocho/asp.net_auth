using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Auth4.Models
{

    public class Files
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int DeptClassId { get; set; }

        public string FileName { get; set; }

        public string FilePathDirectory { get; set; }

        public string ContentType { get; set; }

        public byte[] file { get; set; }

        [Required]
     
        public DateTime CreatedOn { get; set; }
        [Required]
        public string CreatedBy { get; set; }

        public DateTime ModifiedOn { get; set; }


        [ForeignKey("DeptClassId")]
        public virtual DeptClass DeptClass { get; set; }


       
    }
}