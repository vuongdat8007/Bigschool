using Bigschool_TH_11.Controllers.Api;
using Bigschool_TH_11.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using static System.Web.Razor.Parser.SyntaxConstants;

namespace Bigschool_TH_11.Controllers.Api
{
    public class ChucVuApiController : ApiController
    {
        private ApplicationDbContext _context;

        public ChucVuApiController()
        {
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        // GET: api/ChucVuApi
        public IHttpActionResult GetChucVus()
        {
            var chucVus = _context.ChucVus.ToList();
            return Ok(chucVus);
        }
    }
}
