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
        [ResponseType(typeof(BangCapCBNVChuyenNganhViewModel))]
        public IHttpActionResult GetCertificatesByCBNV(string cbnvId)
        {
            CBNV cbnv = db.CBNVs.Include(c => c.CBNVChuyenNganhs.Select(x => x.ChuyenNganh)).SingleOrDefault(c => c.MaCBNV == cbnvId);

            if (cbnv == null)
            {
                return NotFound();
            }

            var certificates = db.BangCapCBNVChuyenNganhs.Include(b => b.ChuyenNganh).Where(x => x.CBNVId == cbnv.MaCBNV).ToList();
            var chuyenNganhs = cbnv.CBNVChuyenNganhs.Select(x => x.ChuyenNganh).ToList();

            var viewModel = new BangCapCBNVChuyenNganhViewModel
            {
                CBNV = cbnv,
                BangCapCBNVChuyenNganhs = certificates,
                ChuyenNganhs = chuyenNganhs
            };

            // Debug information
            var debugInfo = new
            {
                CBNV = cbnv,
                CertificatesCount = certificates.Count,
                ChuyenNganhsCount = chuyenNganhs.Count
            };

            return Ok(viewModel);
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