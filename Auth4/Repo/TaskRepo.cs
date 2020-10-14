using Auth4.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Auth4.Repo
{
    public class TaskRepo
    {

        private ApplicationDbContext _context { get; set; }


        public TaskRepo(ApplicationDbContext context)
        {
            _context = context;

        }

        public void Add(Tasks task)
        {
            _context.Tasks.Add(task);
            _context.SaveChanges();
        }

        public Tasks Get(int ID)
        {
            return _context.Tasks.FirstOrDefault(e => e.Id == ID);
        }

        public IEnumerable<Tasks> GetAll()
        {

            // _context.DeptClasses.ToList<DeptClass>();
            var entity_query = _context.Tasks.ToList();
            return entity_query;
        }

        public IEnumerable<Tasks> GetTaskForActvity(int? id)
        {

            // _context.DeptClasses.ToList<DeptClass>();
            var data = _context.Tasks.Where(e => e.ActvityId == id).ToList<Tasks>();
            return data;
        }



    }
}