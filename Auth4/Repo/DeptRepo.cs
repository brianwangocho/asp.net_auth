using Auth4.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Auth4.Repo
{
    public class DeptRepo
    {
        private ApplicationDbContext _context { get; set; }
      

        public DeptRepo(ApplicationDbContext context)
        {
            _context = context;
     

        }
        public void Add(DeptClass emp)
        {
       
            _context.DeptClasses.Add(emp);
            _context.SaveChanges();
        }
        public DeptClass Get(int ID)
        {
            return _context.DeptClasses.FirstOrDefault(e => e.Id == ID);
        }
        public IEnumerable<DeptClass> GetAll()
        {

            // _context.DeptClasses.ToList<DeptClass>();
            var entity_query = _context.DeptClasses.OrderByDescending(date =>date.CreatedOn).ToList<DeptClass>();
            return entity_query;
        }

    }
}