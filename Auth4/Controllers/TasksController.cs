using Auth4.Models;
using Auth4.Repo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Auth4.Controllers
{
    public class TasksController : Controller
    {

        ApplicationDbContext context = new ApplicationDbContext();
        // GET: Tasks
        public ActionResult Index(int? ActivityId,string Name)
        {

            if (ActivityId.Equals(null))
            {

                return Redirect("/Activity/Index");
            }
            TaskRepo taskRepo = new TaskRepo(context);
            TempData["Activity"] = Name;

            TasksViewModel taskviewmodel = new TasksViewModel()
            {

                tasks = taskRepo.GetTaskForActvity(ActivityId)
            };



            return View(taskviewmodel);
        }
    }
}