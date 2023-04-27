using Bigschool_TH_11.Models;
using Bigschool_TH_11.ViewModels;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.ComponentModel.DataAnnotations;
using System.Web.ApplicationServices;


namespace Bigschool_TH_11.Controllers.Api
{
    public class UserManagementApiController : ApiController
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ApplicationDbContext _dbContext;

        public UserManagementApiController()
        {
            _dbContext = new ApplicationDbContext();
            _userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(_dbContext));
            _roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(_dbContext));
        }

        [HttpGet]
        [Route("api/UserManagementApi/Get")]
        public IHttpActionResult Get()
        {
            var users = _userManager.Users;
            var roles = _roleManager.Roles;
            var quyenTruyCaps = _dbContext.QuyenTruyCaps;
            var chucNangs = _dbContext.ChucNangs;

            var result = new
            {
                Users = users.Select(u => new
                {
                    u.Id,
                    u.UserName,
                    u.Email,
                    Roles = u.Roles.Select(r => new { r.RoleId }),
                    u.CBNVId,
                    u.CBNV
                }),
                Roles = roles.Select(r => new
                {
                    r.Id,
                    r.Name
                }),
                QuyenTruyCaps = quyenTruyCaps.Select(q => new
                {
                    q.MaQuyen,
                    q.TenQuyen,
                    q.MaChucNang
                }),
                ChucNangs = chucNangs.Select(c => new
                {
                    c.MaChucNang,
                    c.TenChucNang
                })
            };

            return Ok(result);
        }


        // GET api/UserManagementApi/Users
        [HttpGet]
        [Route("api/usermanagementapi/users")]
        public IEnumerable<ApplicationUser> Users()
        {
            return _userManager.Users.ToList();
        }

        // GET api/UserManagementApi/GetUser/5
        [HttpGet]
        [Route("api/UserManagementApi/GetUser/{id}")]
        public async Task<IHttpActionResult> GetUser(string id)
        {
            var user = await _userManager.FindByIdAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }

        // POST api/UserManagementApi/CreateOrUpdateUser
        [HttpPost]
        [Route("api/UserManagementApi/CreateOrUpdateUser/{request}")]
        public async Task<IHttpActionResult> CreateOrUpdateUser(CreateOrUpdateUserRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = await _userManager.FindByNameAsync(request.UserName);
            // Fetch the CBNV object using the provided ID
            var cbnv = await _dbContext.CBNVs.FindAsync(request.CBNVId);
            if (user == null)
            {
                user = new ApplicationUser
                {
                    UserName = request.UserName,
                    Name = request.Name,
                    Email = request.Email,
                    CBNV = cbnv
                };

                var result = await _userManager.CreateAsync(user, request.Password);

                if (!result.Succeeded)
                {
                    return GetErrorResult(result);
                }
            }
            else
            {
                user.Name = request.Name;
                user.Email = request.Email;
                user.CBNV = cbnv;

                var result = await _userManager.UpdateAsync(user);

                if (!result.Succeeded)
                {
                    return GetErrorResult(result);
                }
            }

            return Ok();
        }

        [HttpDelete]
        [Route("api/usermanagementapi/DeleteUser/{id}")]
        public async Task<IHttpActionResult> DeleteUser(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            // Remove related records in Attendances table
            var attendancesToRemove = _dbContext.Attendances.Where(a => a.AttendeeId == id).ToList();
            foreach (var attendance in attendancesToRemove)
            {
                _dbContext.Attendances.Remove(attendance);
            }
            await _dbContext.SaveChangesAsync();

            // Proceed to delete the user
            var result = await _userManager.DeleteAsync(user);
            if (!result.Succeeded)
            {
                return GetErrorResult(result);
            }

            return Ok();
        }

        // GET api/UserManagementApi/Roles
        [HttpGet]
        public IEnumerable<IdentityRole> Roles()
        {
            return _roleManager.Roles.ToList();
        }

        // GET api/UserManagementApi/Role/5
        [HttpGet]
        public async Task<IHttpActionResult> Role(string id)
        {
            var role = await _roleManager.FindByIdAsync(id);

            if (role == null)
            {
                return NotFound();
            }

            return Ok(role);
        }

        // POST api/UserManagementApi/CreateOrUpdateRole
        [HttpPost]
        public async Task<IHttpActionResult> CreateOrUpdateRole(CreateOrUpdateRoleRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var role = await _roleManager.FindByNameAsync(request.Name);

            if (role == null)
            {
                role = new IdentityRole
                {
                    Name = request.Name
                };

                var result = await _roleManager.CreateAsync(role);

                if (!result.Succeeded)
                {
                    return GetErrorResult(result);
                }
            }
            else
            {
                role.Name = request.Name;

                var result = await _roleManager.UpdateAsync(role);

                if (!result.Succeeded)
                {
                    return GetErrorResult(result);
                }
            }

            return Ok();
        }

        // DELETE api/UserManagementApi/DeleteRole/5
        [HttpDelete]
        public async Task<IHttpActionResult> DeleteRole(string id)
        {
            var role = await _roleManager.FindByIdAsync(id);

            if (role == null)
            {
                return NotFound();
            }

            var result = await _roleManager.DeleteAsync(role);

            if (!result.Succeeded)
            {
                return GetErrorResult(result);
            }

            return Ok();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _dbContext.Dispose();
                _userManager.Dispose();
                _roleManager.Dispose();
            }

            base.Dispose(disposing);
        }

        private IHttpActionResult GetErrorResult(IdentityResult result)
        {
            if (result == null)
            {
                return InternalServerError();
            }

            if (!result.Succeeded)
            {
                if (result.Errors != null)
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error);
                    }
                }

                if (ModelState.IsValid)
                {
                    // No ModelState errors are available to send, so just return an empty BadRequest.
                    return BadRequest();
                }

                return BadRequest(ModelState);
            }

            return null;
        }
    }
    public class CreateOrUpdateUserRequest
    {
        public string UserName { get; set; }

        [Required]
        [StringLength(255)]
        public string Name { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        public string Password { get; set; }

        public string CBNVId { get; set; }
    }

    public class CreateOrUpdateRoleRequest
    {
        public string Name { get; set; }
    }
}
