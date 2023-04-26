using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Http;
using System.Web.Optimization;
using System.Web.Routing;
using WebGrease.Configuration;
using Bigschool_TH_11.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity;

namespace Bigschool_TH_11
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            //UnityConfig.RegisterComponents();
            // Create the super admin user
            CreateSuperAdmin();
        }

        private void CreateSuperAdmin()
        {
            using (var context = new ApplicationDbContext())
            {
                var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
                var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));

                const string adminRoleName = "SuperAdmin";
                const string adminUserName = "superadmin@example.com";
                const string adminPassword = "SuperAdmin123!";
                const string adminName = "Super Admin"; // Add this line

                // Create the SuperAdmin role if it doesn't exist
                if (!roleManager.RoleExists(adminRoleName))
                {
                    var role = new IdentityRole(adminRoleName);
                    roleManager.Create(role);
                }

                // Create the super admin user if it doesn't exist
                var user = userManager.FindByName(adminUserName);
                if (user == null)
                {
                    user = new ApplicationUser { UserName = adminUserName, Email = adminUserName, Name = adminName };
                    userManager.Create(user, adminPassword);
                    userManager.SetLockoutEnabled(user.Id, false);
                }
                // Add the super admin user to the SuperAdmin role
                var rolesForUser = userManager.GetRoles(user.Id);
                if (!rolesForUser.Contains(adminRoleName))
                {
                    userManager.AddToRole(user.Id, adminRoleName);
                }
            }
        }
    }

    
}
