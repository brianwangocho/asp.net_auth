using Auth4.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Auth4.Repo
{
    public class PertakeRepo
    {

        private ApplicationDbContext _context { get; set; }


        public PertakeRepo(ApplicationDbContext context)
        {
            _context = context;

        }


        public void Add(Pertake pertake)
        {
            _context.Pertake.Add(pertake);
            _context.SaveChanges();

        }


    }
}