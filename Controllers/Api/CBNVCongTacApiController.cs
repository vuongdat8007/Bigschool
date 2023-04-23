using Bigschool_TH_11.Models;
using Bigschool_TH_11.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;

namespace Bigschool_TH_11.Controllers.Api
{
    public class CBNVCongTacApiController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/CBNVCongTacApi
        [ResponseType(typeof(CBNVCongTacListViewModel))]
        public async Task<IHttpActionResult> GetCBNVCongTacs()
        {
            var cbnvCongTacs = await db.CBNVCongTacs.Include(c => c.CBNV).Include(c => c.ChucVu).ToListAsync();
            var cbnvs = await db.CBNVs.ToListAsync();
            var chucVusAvailable = await db.ChucVus.ToListAsync();
            var viewModel = new CBNVCongTacListViewModel
            {
                CBNVCongTacs = cbnvCongTacs,
                CBNVs = cbnvs,
                ChucVuAvails = chucVusAvailable
            };

            return Ok(viewModel);
        }

        // GET: api/CBNVCongTacApi/5
        [ResponseType(typeof(CBNVCongTacViewModel))]
        public async Task<IHttpActionResult> GetCBNVCongTac(int id) // Change the input parameter to int
        {
            CBNVCongTac cbnvCongTac = await db.CBNVCongTacs.Include(c => c.CBNV).Include(c => c.ChucVu).SingleOrDefaultAsync(c => c.CBNVCongTacId == id); // Fetch the CBNVCongTac record by id
            if (cbnvCongTac == null)
            {
                return NotFound();
            }

            ChucVu relatedChucVu = cbnvCongTac.ChucVu; // Access the related ChucVu via the navigation property

            CBNVCongTacViewModel cbnvCongTacViewModel = new CBNVCongTacViewModel
            {
                CBNVCongTac = cbnvCongTac,
                ChucVu = relatedChucVu
            };

            return Ok(cbnvCongTacViewModel);
        }

        // PUT: api/CBNVCongTacApi/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutCBNVCongTac(string id, CBNVCongTac cbnvCongTac)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != cbnvCongTac.MaCBNV)
            {
                return BadRequest();
            }

            db.Entry(cbnvCongTac).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CBNVCongTacExists(id))
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

        // POST: api/CBNVCongTacApi
        [ResponseType(typeof(CBNVCongTac))]
        public async Task<IHttpActionResult> PostCBNVCongTac(CBNVCongTac cbnvCongTac)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.CBNVCongTacs.Add(cbnvCongTac);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = cbnvCongTac.MaCBNV }, cbnvCongTac);
        }

        // DELETE: api/CBNVCongTacApi/5
        [ResponseType(typeof(CBNVCongTac))]
        public async Task<IHttpActionResult> DeleteCBNVCongTac(string id)
        {
            CBNVCongTac cbnvCongTac = await db.CBNVCongTacs.FindAsync(id);
            if (cbnvCongTac == null)
            {
                return NotFound();
            }

            db.CBNVCongTacs.Remove(cbnvCongTac);
            await db.SaveChangesAsync();

            return Ok(cbnvCongTac);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CBNVCongTacExists(string id)
        {
            return db.CBNVCongTacs.Count(e => e.MaCBNV == id) > 0;
        }
    }
}