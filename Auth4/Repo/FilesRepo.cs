using Auth4.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Auth4.Repo
{
    public class FilesRepo
    {
        private ApplicationDbContext _context { get; set; }


        public FilesRepo(ApplicationDbContext context)
        {
            _context = context;

        }
        public void Add(Files files)
        {
            _context.Files.Add(files);
            _context.SaveChanges();
        }
        public IEnumerable<Files> GetClassFiles(string DeptclassId)
        {
          var classFiles =   _context.Files.Where(e => e.DeptClassId == DeptclassId).ToList<Files>();
           return classFiles;
        }
}
}