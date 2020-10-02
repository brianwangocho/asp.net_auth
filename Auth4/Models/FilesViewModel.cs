using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Auth4.Models
{
    public class FilesViewModel
    {
      public  IEnumerable<Files> files { get; set; }



        public bool IsListEmpty()
            
        {
            bool isEmpty = false;
            IEnumerable<Files> data = this.files;

            if (!data.Any())
            {
                isEmpty = true;
                return isEmpty;
            }
            return isEmpty;
        }
    }
}