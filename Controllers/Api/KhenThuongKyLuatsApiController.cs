using Bigschool_TH_11.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Bigschool_TH_11.Controllers.Api
{
    public class KhenThuongKyLuatsApiController : ApiController
    {
        private ApplicationDbContext _context;

        public KhenThuongKyLuatsApiController()
        {
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _context.Dispose();
            }
            base.Dispose(disposing);
        }
        // GET: api/KhenThuongKyLuatsApi
        public IHttpActionResult GetKhenThuongKyLuats()
        {
            var khenThuongKyLuats = _context.KhenThuongKyLuats;
            return Ok(khenThuongKyLuats);
        }
    }
}
