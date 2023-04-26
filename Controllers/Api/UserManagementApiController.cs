using Bigschool_TH_11.Models;
using Bigschool_TH_11.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Bigschool_TH_11.Controllers.Api
{
    public class UserManagementApiController : ApiController
    {
        private ApplicationDbContext _context;

        public UserManagementApiController()
        {
            _context = new ApplicationDbContext();
        }

        // GET: api/UserManagementApi
        public IHttpActionResult GetUserManagementData()
        {
            var users = _context.Users.ToList();
            var roles = _context.Roles.ToList();
            var chucNangs = _context.ChucNangs.ToList();
            var quyenTruyCaps = _context.QuyenTruyCaps.ToList();

            var viewModel = new UserManagementViewModel
            {
                Users = users,
                Roles = roles,
                ChucNangs = chucNangs,
                QuyenTruyCaps = quyenTruyCaps
            };

            return Ok(viewModel);
        }
    }
}
