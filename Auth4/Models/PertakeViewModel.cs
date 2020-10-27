using Auth4.Repo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Auth4.Models
{
    public class PertakeViewModel
    {
        ApplicationDbContext context = new ApplicationDbContext();
        public IEnumerable<Rotter> rotterlist { get; set; }




        public string getName(string id)
        {
            ApplicationUser user = context.Users.Where(e => e.Id == id).FirstOrDefault();

            return user.UserName;

        }

        public string getTask(int id)
        {
            TaskRepo taskRepo = new TaskRepo(context);

            return taskRepo.Get(id).Name;

        }

        public bool IsListEmpty()

        {
            bool isEmpty = false;
            IEnumerable<Rotter> data = this.rotterlist;

            if (!data.Any())
            {
                isEmpty = true;
                return isEmpty;
            }
            return isEmpty;
        }

    }
}