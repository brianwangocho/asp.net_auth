using Auth4.Models;
using Auth4.Repo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Auth4.Controllers
{
    public class PertakeController : Controller
    {
        ApplicationDbContext context = new ApplicationDbContext();
        // GET: Pertake
        public ActionResult Index()
        {

            ActivityRepo activityRepo = new ActivityRepo(context);
            RotterRepo rotterRepo = new RotterRepo(context);
            ViewBag.Activity = activityRepo.selecItemActivity();
            PertakeViewModel pertakeViewModel = new PertakeViewModel()
            {
                rotterlist = rotterRepo.GetAll()
        };

            return View(pertakeViewModel);
        }

      
        [HttpPost]
        public ActionResult AssignPersonToActivity(FormCollection form)
        {
            string activity = form["Activity"];
            string userName = form["txtUserName"];
            string name = form["Name"];
            PertakeRepo pertakeRepo = new PertakeRepo(context);
            RotterRepo rotterRepo = new RotterRepo(context);
            ApplicationUser user = context.Users.Where(e => e.UserName == userName).FirstOrDefault();

            Pertake pertake = new Pertake()
            {
                UserId = user.Id,
                status = "1",
                name = name.ToLower().Trim(),
                ActivityId= Int32.Parse(activity)
            };
            List<Rotter> rotters = new List<Rotter>();

            ActivityRepo activityRepo = new ActivityRepo(context);
            int size = activityRepo.Get(Int32.Parse(activity)).Tasks.Count;
            List<Tasks> tasks = activityRepo.Get(Int32.Parse(activity)).Tasks;
            
            Console.WriteLine(tasks);
           

            foreach (Tasks task in tasks)
            {
                Rotter rotter = new Rotter()
                {
                    PertakerId = user.Id,
                    status = "1",
                    TaskId = task.Id,
                    UserId = task.AssignTo,
                    StartedOn = DateTime.Now,
                    EndedOn = DateTime.Now,
                };




                rotterRepo.add(rotter);
            }



          
           
            pertakeRepo.Add(pertake);
          








            return RedirectToAction("Index");
        }

    }
}