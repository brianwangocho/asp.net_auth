using Auth4.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Auth4.Repo
{
    public class RotterRepo
    {

        private ApplicationDbContext _context { get; set; }


        public RotterRepo(ApplicationDbContext context)
        {
            _context = context;

        }

        public void add(Rotter rotter)  
        {

            _context.Rotters.Add(rotter);
            _context.SaveChanges();



        }

        public IEnumerable<Rotter> GetAll()
        {

            // _context.DeptClasses.ToList<DeptClass>();
            var entity_query = _context.Rotters.ToList<Rotter>();
            return entity_query;
        }
    }
}