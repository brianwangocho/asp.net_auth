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
            TempData["ActivityId"] = ActivityId.ToString();
            ViewBag.Users = context.Users.Select(r => new SelectListItem { Value = r.Id, Text = r.UserName }).ToList();
            TasksViewModel taskviewmodel = new TasksViewModel()
            {

                tasks = taskRepo.GetTaskForActvity(ActivityId)
            };



            return View(taskviewmodel);
        }



        [HttpPost]
        public ActionResult CreateTask(FormCollection form)
        {
            string Name = form["TaskName"];
            string Priority = form["TaskPriority"];
            string AssignUser = form["User"];
            string id = form["id"];
            TaskRepo taskRepo = new TaskRepo(context);
            if ( taskRepo.findIfPriorityandNameExists(Int32.Parse(Priority),Name.ToLower()) )
            {
                SetFlash(FlashMessageType.Error, "this priority is already assigned to a task");

            }
            else
            {
                Tasks task = new Tasks()
                {
                    Name = Name.ToLower(),
                    Priority = Int32.Parse(Priority),
                    ActvityId = Int32.Parse(id),
                    AssignTo = AssignUser,
                    Status = "1"
                };

                taskRepo.Add(task);
                SetFlash(FlashMessageType.Success, "Task has been successfuly added");
            }

           



           


          return  RedirectToAction("Index");
        }

        public void SetFlash(FlashMessageType type, string text)
        {
            TempData["FlashMessage.Type"] = type;
            TempData["FlashMessage.Text"] = text;
        }
        

        [HttpGet]
        public PartialViewResult Edit(int taskId)
        {
            TaskRepo taskRepo = new TaskRepo(context);
            Tasks tasks = taskRepo.Get(taskId);
            ViewBag.Users = context.Users.Select(r => new SelectListItem { Value = r.Id, Text = r.UserName ,Selected = r.Id == tasks.AssignTo}).ToList();

            return PartialView(tasks);

        }
        [HttpPost]
        public JsonResult Edit(Tasks task)
        {
            TaskRepo taskRepo = new TaskRepo(context);
            Tasks data = taskRepo.Get(task.Id);
            Tasks tasks = new Tasks()
            {
              Id = data.Id,
              ActvityId = data.ActvityId,
              Name = data.Name,
              AssignTo = data.AssignTo,
              Status =data.Status,
              Priority = data.Priority
            };





            return Json(data, JsonRequestBehavior.AllowGet);
        }



    }
}