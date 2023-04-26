using Bigschool_TH_11.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity;
using System.Web.Mvc;
using Unity;
using Unity.Mvc5;
using System.Data.Entity;
using Unity.Injection;

namespace Bigschool_TH_11
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
            var container = new UnityContainer();

            // Register your dependencies here
            container.RegisterType<ApplicationRoleManager>();
            container.RegisterType<IRoleStore<IdentityRole, string>, RoleStore<IdentityRole>>();
            
            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }
    }
}