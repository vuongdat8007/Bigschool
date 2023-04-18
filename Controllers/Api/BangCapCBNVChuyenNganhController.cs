using Bigschool_TH_11.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Data.Entity;
using System.Linq;
using System.Net;

using System.Threading.Tasks;
//using System.Web.Mvc;
using System.Web.Http;
using Bigschool_TH_11.ViewModels;
using System.Web.Http.Description;

namespace Bigschool_TH_11.Controllers.Api
{
    public class BangCapCBNVChuyenNganhController : System.Web.Http.ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public IQueryable<BangCapCBNVChuyenNganh> GetBangCapCBNVChuyenNganhs()
        {
            return db.BangCapCBNVChuyenNganhs;
        }

        // GET: api/BangCapCBNVChuyenNganh/{cbnvId}
        [ResponseType(typeof(BangCapCBNVChuyenNganh))]
        public IHttpActionResult GetCertificatesByCBNV(string cbnvId)
        {
            var certificates = db.BangCapCBNVChuyenNganhs.Where(x => x.CBNVId == cbnvId).ToList();
            return Ok(certificates);
        }

        // POST: api/BangCapCBNVChuyenNganh
        [HttpPost]
        public IHttpActionResult PostCertificate(BangCapCBNVChuyenNganh certificate)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.BangCapCBNVChuyenNganhs.Add(certificate);
            db.SaveChanges();

            return Ok(certificate);
        }

        // PUT: api/BangCapCBNVChuyenNganh/{id}
        [HttpPut]
        public IHttpActionResult PutCertificate(int id, BangCapCBNVChuyenNganh certificate)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != certificate.Id)
            {
                return BadRequest();
            }

            db.Entry(certificate).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CertificateExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // DELETE: api/BangCapCBNVChuyenNganh/{id}
        [HttpDelete]
        public IHttpActionResult DeleteCertificate(int id)
        {
            BangCapCBNVChuyenNganh certificate = db.BangCapCBNVChuyenNganhs.Find(id);
            if (certificate == null)
            {
                return NotFound();
            }

            db.BangCapCBNVChuyenNganhs.Remove(certificate);
            db.SaveChanges();

            return Ok(certificate);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CertificateExists(int id)
        {
            return db.BangCapCBNVChuyenNganhs.Count(e => e.Id == id) > 0;
        }
    }
}