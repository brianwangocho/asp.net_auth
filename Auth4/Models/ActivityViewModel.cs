using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Auth4.Models
{
    public class ActivityViewModel
    {

        public IEnumerable<Activity> activities { get; set; }

        public string message { get; set; }

        public bool IsListEmpty()

        {
            bool isEmpty = false;
            IEnumerable<Activity> data = this.activities;

            if (!data.Any())
            {
                isEmpty = true;
                return isEmpty;
            }
            return isEmpty;
        }


    }
}