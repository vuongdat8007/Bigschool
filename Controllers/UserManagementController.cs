using Bigschool_TH_11.Models;
using Bigschool_TH_11.ViewModels;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using Microsoft.AspNet.Identity.Owin;
using Unity;
using Unity.Mvc5;

namespace Bigschool_TH_11.Controllers
{
    public class UserManagementController : Controller
    {
        private readonly ApplicationDbContext _context;
        /*private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        public UserManagementController(
            ApplicationDbContext context,
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
        }*/

        private readonly ApplicationRoleManager _roleManager;
        private readonly ApplicationUserManager _userManager;

        public UserManagementController()
        {
            _context = new ApplicationDbContext();
            _roleManager = new ApplicationRoleManager(new RoleStore<IdentityRole>(_context));
            _userManager = new ApplicationUserManager(new UserStore<ApplicationUser>(_context));
        }

        /*private readonly ApplicationRoleManager _roleManager;
        public UserManagementController(ApplicationRoleManager roleManager)
        {
            _roleManager = roleManager;
        }*/

        [HttpGet]
        [Authorize(Roles = "SuperAdmin")]
        public async Task<ActionResult> Index()
        {
            var viewModel = new UserManagementViewModel
            {
                Users = await _userManager.Users.ToListAsync(),
                Roles = await _roleManager.Roles.ToListAsync(),
                ChucNangs = await _context.ChucNangs.ToListAsync(),
                QuyenTruyCaps = await _context.QuyenTruyCaps.ToListAsync()
            };

            return View(viewModel);
        }

        [HttpGet]
        [Authorize(Roles = "SuperAdmin")]
        public async Task<ActionResult> Manage()
        {
            var viewModel = new UserManagementViewModel
            {
                Users = await _userManager.Users.ToListAsync(),
                Roles = await _roleManager.Roles.ToListAsync(),
                ChucNangs = await _context.ChucNangs.ToListAsync(),
                QuyenTruyCaps = await _context.QuyenTruyCaps.ToListAsync()
            };

            return View(viewModel);
        }

        // Add other action methods for creating, updating, and deleting roles and users.
        public async Task<ActionResult> UserDetails(string id)
        {
            var userManager = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            var user = await userManager.FindByIdAsync(id);
            return PartialView("_UserDetails", user);
        }

        public async Task<ActionResult> RoleDetails(string id)
        {
            var roleManager = HttpContext.GetOwinContext().Get<ApplicationRoleManager>();
            var role = await roleManager.FindByIdAsync(id);
            return PartialView("_RoleDetails", role);
        }

        // Edit user
        [HttpPost]
        public async Task<ActionResult> EditUser(ApplicationUser user)
        {
            if (ModelState.IsValid)
            {
                var result = await _userManager.UpdateAsync(user);
                if (result.Succeeded)
                {
                    return Json(new { success = true });
                }
            }
            return Json(new { success = false });
        }

        // Delete user
        [HttpPost]
        public async Task<ActionResult> DeleteUser(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user != null)
            {
                var result = await _userManager.DeleteAsync(user);
                if (result.Succeeded)
                {
                    return Json(new { success = true });
                }
            }
            return Json(new { success = false });
        }

        // Edit role
        [HttpPost]
        public async Task<ActionResult> EditRole(IdentityRole role)
        {
            if (ModelState.IsValid)
            {
                var result = await _roleManager.UpdateAsync(role);
                if (result.Succeeded)
                {
                    return Json(new { success = true });
                }
            }
            return Json(new { success = false });
        }

        // Delete role
        [HttpPost]
        public async Task<ActionResult> DeleteRole(string id)
        {
            var role = await _roleManager.FindByIdAsync(id);
            if (role != null)
            {
                var result = await _roleManager.DeleteAsync(role);
                if (result.Succeeded)
                {
                    return Json(new { success = true });
                }
            }
            return Json(new { success = false });
        }
    }
}