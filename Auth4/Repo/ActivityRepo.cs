using Auth4.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Auth4.Repo
{
    public class ActivityRepo
    {

        private ApplicationDbContext _context { get; set; }


        public ActivityRepo(ApplicationDbContext context)
        {
            _context = context;


        }

        public void Add(Activity activity)
        {
            _context.Activities.Add(activity);
            _context.SaveChanges();
        }

        public bool findByName(string name)
        {
            bool Exist = false;
            var data = _context.Activities.Where(b => b.Name == name).FirstOrDefault();

            if (data != null)
            {
                Exist = true;
                return Exist;
            }

            return Exist;
        }


        public Activity Get(int ID)
        {
            return _context.Activities.FirstOrDefault(e => e.Id == ID);
        }

        public void DeactivateActivity(int ID)
        {
            Activity activity = _context.Activities.FirstOrDefault(e => e.Id == ID);
            activity.Status = "0";
            _context.Set<Activity>().AddOrUpdate(activity);
            _context.SaveChanges();
        }
        public void ActivateActivity(int ID)
        {
            Activity activity = _context.Activities.FirstOrDefault(e => e.Id == ID);
            activity.Status = "1";
            _context.Set<Activity>().AddOrUpdate(activity);
            _context.SaveChanges();
        }



        public List<SelectListItem> selecItemActivity()
        {
           var data = _context.Activities.Select(r => new SelectListItem { Value = r.Id.ToString(), Text = r.Name }).ToList();
            return data;
        }

        public IEnumerable<Activity> GetAll()
        {

            // _context.DeptClasses.ToList<DeptClass>();
            var entity_query = _context.Activities.OrderByDescending(date => date.CreatedOn).ToList<Activity>();
            return entity_query;
        }
    }
  
}