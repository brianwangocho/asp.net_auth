using Auth4.Models;
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
            return _context.DeptClasses.ToList<DeptClass>();
        }

    }
}