using Auth4.Models;
using Auth4.Repo;
using Microsoft.AspNet.Identity;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Auth4.Controllers
{
    public class ActivityController : Controller
    {

        ApplicationDbContext context = new ApplicationDbContext();




     
        // GET: Activity
        
        public ActionResult Index()
        {
            ActivityRepo activityRepo = new ActivityRepo(context);
            var data = activityRepo.GetAll();
            ActivityViewModel activityviewmodel = new ActivityViewModel()
            {
       
                activities = data
            };

            return View(activityviewmodel);
        }


        [HttpPost]
        public ActionResult CreateActivity(FormCollection form)
        {
            ActivityRepo activityRepo = new ActivityRepo(context);
            string Name = form["ActivityName"];
            Activity activity = new Activity()
            {
                Name = Name.ToLower(),
                Status = "1",
                CreatedBy = (string)User.Identity.GetUserId(),
                CreatedOn = DateTime.Now
            };

            if (activityRepo.findByName(Name))
            {
                SetFlash(FlashMessageType.Error, "this  data by this name exists");

            }
            else
            {
                activityRepo.Add(activity);
                SetFlash(FlashMessageType.Success, "Activity has been added");
            }

           

            



            return RedirectToAction("Index");
        }

        [HttpPost]
        public JObject DeactivateActivity(int id)
        {

            ActivityRepo activityRepo = new ActivityRepo(context);
            activityRepo.DeactivateActivity(id);
            JObject jObject = new JObject();
            jObject.Add("status","00");
            jObject.Add("message", "success");

            return  jObject ;
        }

        [HttpPost]
        public JObject ActivateActivity(int id)
        {

            ActivityRepo activityRepo = new ActivityRepo(context);
            activityRepo.ActivateActivity(id);
            JObject jObject = new JObject();
            jObject.Add("status", "00");
            jObject.Add("message", "success");

            return jObject;
        }


        public void SetFlash(FlashMessageType type, string text)
        {
            TempData["FlashMessage.Type"] = type;
            TempData["FlashMessage.Text"] = text;
        }




      
    }

    

}