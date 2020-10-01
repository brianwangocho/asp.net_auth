using DevExpress.Web.Mvc;
using Auth4.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Auth4.Repo;

namespace Auth4.Controllers
{
    public class AdminController : Controller
    {

        ApplicationDbContext context = new ApplicationDbContext();


        // GET: Admin
        [Authorize(Roles = "Admin")]
        public ActionResult Index(int page = 1)
        {
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
            /// get all users
            List<UserViewModel> model = new List<UserViewModel>();
            model = userManager.Users.Select(u => new UserViewModel
            {
                Id = u.Id,
                Name = u.UserName,
                Email = u.UserName,
                EmailConfirmed = u.EmailConfirmed
            }).ToList();

            DeptRepo deptRepo = new DeptRepo(context);
            deptRepo.GetAll();
            var data = new DeptClassViewModel
            {
                ClassesPerPage = 10,
                DeptClasses = deptRepo.GetAll(),
                CurrentPage = page
            };



            return View(data);
        }

        [HttpPost]
        public ActionResult CreateUser(FormCollection form)
        {
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
            string Username = form["txtEmail"];
            string Email = form["txtEmail"];
            string Password = form["password"];

            var user = new ApplicationUser();
            user.Email = Email;
            user.UserName = Username;

            var newUser = userManager.Create(user, Password);
            if (newUser.Succeeded)
            {
                return Redirect("/");
            }
            return Redirect("/");
        }
        public ActionResult AssignRole()
        {
            ViewBag.Roles = context.Roles.Select(r => new SelectListItem { Value = r.Name, Text = r.Name }).ToList();
            return View();
        }

        [HttpPost]
        public ActionResult AssignRole(FormCollection form)
        {
            string user = form["txtUserName"];
            string role = form["RoleName"];
            return View("Index");

        }
        public ActionResult CreateRole()

        {
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            List<RolesViewModel> model = new List<RolesViewModel>();
            model = roleManager.Roles.Select(e => new RolesViewModel
            {
                Id = e.Id,
                Name = e.Name,
                Count = e.Users.Count
            }).ToList();

            return View(model);
        }

        [HttpPost]
        public ActionResult CreateDeptClass(FormCollection form)
        {
          using(var context1 = new ApplicationDbContext()){

              
                    var data = new DeptClass()
                    {
                        CreatedBy = (string)User.Identity.GetUserId(),
                        CreatedOn = DateTime.Now,
                        ModifiedOn = DateTime.Now,
                        Name = "New class",
                        Description = "new Description"

                    };
                    DeptRepo deptRepo = new DeptRepo(context1);
                    deptRepo.Add(data);
              
          

            }
          

       

            return Redirect("/Admin/Index");
        }



        [HttpPost]
        public ActionResult CreateRole(FormCollection form)

        {
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            string rolename = form["RoleName"];
            bool exists = roleManager.RoleExists(rolename);

            if (!exists)
            {
                var role = new IdentityRole(rolename);
                roleManager.Create(role);

                return Redirect("/");
            }
            return Redirect("/");
        }



     
    }



}