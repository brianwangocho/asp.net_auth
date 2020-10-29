using Auth4.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Owin;
using System;

[assembly: OwinStartupAttribute(typeof(Auth4.Startup))]
namespace Auth4
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            CreateUserAndRoles();
          
        }


        public  void CreateUserAndRoles()
        {
            //// get the db context
            ApplicationDbContext context = new ApplicationDbContext();

            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
            
            bool exists = roleManager.RoleExists("Admin");

            if (!exists)
            {
                /// if this role doesnt exist create it
                var adminrole = new IdentityRole("Admin");
                roleManager.Create(adminrole);

                /// create default user
                var user = new ApplicationUser();
                user.UserName = "jack@gmail.com";
                user.EmailConfirmed = false;
                user.PhoneNumber = null;
                user.AccessFailedCount = 0;
                user.LockoutEnabled = true;
                user.LockoutEndDateUtc = null;
                user.TwoFactorEnabled = false;
                user.Email = "jack@gmail.com";
                string pass = "J@ck123";
                

                //// takes two params IdentityUser and password
                var adminuser = userManager.Create(user, pass);
                if (adminuser.Succeeded)
                {
                    userManager.AddToRoles(user.Id,"Admin");
                }
            }
            if (!roleManager.RoleExists("StaffOfficer"))
            {
                /// if this role doesnt exist create it
                var userrole = new IdentityRole("StaffOfficer");
                roleManager.Create(userrole);

                /// create default user
                var user1 = new ApplicationUser();
                user1.UserName = "user@gmail.com";
                user1.EmailConfirmed = false;
                user1.PhoneNumber = null;
                user1.AccessFailedCount = 0;
                user1.LockoutEnabled = true;
                user1.LockoutEndDateUtc = null;
                user1.TwoFactorEnabled = false;
                user1.Email = "user@gmail.com";
                string pass = "User@123";

                var newuser = userManager.Create(user1, pass);
                if (newuser.Succeeded)
                {
                    userManager.AddToRoles(user1.Id, "StaffOfficer");
                }
            }
            if (!roleManager.RoleExists("StaffAdmin"))
            {
                /// if this role doesnt exist create it
                var userrole = new IdentityRole("StaffAdmin");
                roleManager.Create(userrole);

                /// create default user
                var user2 = new ApplicationUser();
                user2.UserName = "userAdmin@gmail.com";
                user2.EmailConfirmed = false;
                user2.PhoneNumber = null;
                user2.AccessFailedCount = 0;
                user2.LockoutEnabled = true;
                user2.LockoutEndDateUtc = null;
                user2.TwoFactorEnabled = false;
                user2.Email = "userAdmin@gmail.com";
                string pass = "UserAdmin@123";

                var newuser = userManager.Create(user2, pass);
                if (newuser.Succeeded)
                {
                    userManager.AddToRoles(user2.Id, "StaffAdmin");
                }
            }

        }
    }
}
