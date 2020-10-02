using Auth4.Models;
using Auth4.Repo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Auth4.Controllers
{
    [Authorize]
    public class FilesController : Controller
    {
        ApplicationDbContext context = new ApplicationDbContext();
        // GET: Files
        public ActionResult Index(string DeptClassId)
        {
            FilesRepo filesRepo = new FilesRepo(context);
            FilesViewModel filesviewmodel = new FilesViewModel()
            {
                files = filesRepo.GetClassFiles(DeptClassId)
        };

            return View(filesviewmodel);
        }
    }
}