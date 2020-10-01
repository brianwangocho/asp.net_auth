using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Auth4.Models
{
    public class DeptClassViewModel
    {
        public IEnumerable<DeptClass> DeptClasses { get; set; }
        public int ClassesPerPage { get; set; }
        public int CurrentPage { get; set; }


        public int PageCount()
        {
            return Convert.ToInt32(Math.Ceiling(DeptClasses.Count() / (double)ClassesPerPage));
        }
        public IEnumerable<DeptClass> PaginatedBlogs()
        {
            int start = (CurrentPage - 1) * ClassesPerPage;
            return DeptClasses.OrderBy(b => b.CreatedOn).Skip(start).Take(ClassesPerPage);
        }
    }
}