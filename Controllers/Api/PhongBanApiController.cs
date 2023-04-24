using Bigschool_TH_11.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace Bigschool_TH_11.Controllers.Api
{
    public class PhongBanApiController : ApiController
    {
        private ApplicationDbContext _context;

        public PhongBanApiController()
        {
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        // GET: api/PhongBanApi
        public IHttpActionResult GetPhongBans()
        {
            var phongBans = _context.PhongBans.ToList();
            return Ok(phongBans);
        }
    }
}