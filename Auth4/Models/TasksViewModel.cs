using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Auth4.Models
{
    public class TasksViewModel
    {

        public IEnumerable<Tasks> tasks { get; set; }

        public string message { get; set; }

        public bool IsListEmpty()

        {
            bool isEmpty = false;
            IEnumerable<Tasks> data = this.tasks;

            if (!data.Any())
            {
                isEmpty = true;
                return isEmpty;
            }
            return isEmpty;
        }


    }
}