using Bigschool_TH_11.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;

namespace Bigschool_TH_11.Controllers.Api
{
    public class ChuyenNganhController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/ChuyenNganh
        public IQueryable<ChuyenNganh> GetChuyenNganhs()
        {
            return db.ChuyenNganhs;
        }

        // GET: api/ChuyenNganh/5
        [ResponseType(typeof(ChuyenNganh))]
        public IHttpActionResult GetChuyenNganh(int id)
        {
            ChuyenNganh chuyenNganh = db.ChuyenNganhs.Find(id);
            if (chuyenNganh == null)
            {
                return NotFound();
            }

            return Ok(chuyenNganh);
        }

        // GET: api/ChuyenNganh/CBNV/5
        [HttpGet]
        [Route("api/ChuyenNganh/CBNV/{maCBNV}")]
        public IHttpActionResult GetChuyenNganhsByCBNV(String maCBNV)
        {
            var chuyenNganhs = db.CBNVChuyenNganhs
                                 .Where(x => x.MaCBNV == maCBNV)
                                 .Select(x => x.ChuyenNganh)
                                 .ToList();

            if (chuyenNganhs.Count == 0)
            {
                return NotFound();
            }

            return Ok(chuyenNganhs);
        }

        // POST: api/ChuyenNganh/AddCBNVChuyenNganh
        [HttpPost]
        [Route("api/ChuyenNganh/AddCBNVChuyenNganh")]
        public IHttpActionResult AddCBNVChuyenNganh(String maCBNV, String maChuyenNganh)
        {
            var cbnvChuyenNganh = new CBNVChuyenNganh { MaCBNV = maCBNV, MaChuyenNganh = maChuyenNganh };
            db.CBNVChuyenNganhs.Add(cbnvChuyenNganh);
            db.SaveChanges();

            return Ok(cbnvChuyenNganh);
        }

        // DELETE: api/ChuyenNganh/RemoveCBNVChuyenNganh
        [HttpDelete]
        [Route("api/ChuyenNganh/RemoveCBNVChuyenNganh")]
        public IHttpActionResult RemoveCBNVChuyenNganh(String maCBNV, String maChuyenNganh)
        {
            var cbnvChuyenNganh = db.CBNVChuyenNganhs.FirstOrDefault(x => x.MaCBNV == maCBNV && x.MaChuyenNganh == maChuyenNganh);

            if (cbnvChuyenNganh == null)
            {
                return NotFound();
            }

            db.CBNVChuyenNganhs.Remove(cbnvChuyenNganh);
            db.SaveChanges();

            return Ok(cbnvChuyenNganh);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ChuyenNganhExists(String id)
        {
            return db.ChuyenNganhs.Count(e => e.MaChuyenNganh == id) > 0;
        }
    }
}