using DevExpress.Web.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

        public string ClassName() {

            return null;
        }
        public string Data()
        {
          
            return ("wangocho");


        }

        public string LoadPdf(byte[] file)
        {

            //StringBuilder hex = new StringBuilder(file.Length * 2);
            //foreach (byte b in file)
            //    hex.AppendFormat("{0:x2}", b);


          //  string docBase64 = "data:image/jpeg;base64," +Convert.ToBase64String(file);


          string docBase64 = "data:application/pdf;base64," + Convert.ToBase64String(file);
            return (docBase64);


        }




    }
}