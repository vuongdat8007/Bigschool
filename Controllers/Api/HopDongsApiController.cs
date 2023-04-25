using Bigschool_TH_11.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Mvc;

namespace Bigschool_TH_11.Controllers.Api
{
    public class HopDongsApiController : ApiController
    {   
        private ApplicationDbContext _context;
        public HopDongsApiController()
        {
            _context= new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        // GET: api/HopDongsApi
        public IHttpActionResult GetHopDongs()
        {
            var hopDongs = _context.HopDongs.ToList();
            return Ok(hopDongs);
        }
    }
}