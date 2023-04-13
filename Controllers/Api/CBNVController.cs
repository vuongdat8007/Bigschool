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
using Bigschool_TH_11.Models;

namespace Bigschool_TH_11.Controllers.Api
{
    public class CBNVController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/CBNV
        public IQueryable<CBNV> GetCBNVs()
        {
            return db.CBNVs;
        }

        // GET: api/CBNV/5
        [ResponseType(typeof(CBNV))]
        public async Task<IHttpActionResult> GetCBNV(string id)
        {
            CBNV cbnv = await db.CBNVs.FindAsync(id);
            if (cbnv == null)
            {
                return NotFound();
            }

            return Ok(cbnv);
        }

        // PUT: api/CBNV/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutCBNV(string id, CBNV cbnv)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != cbnv.MaCBNV)
            {
                return BadRequest();
            }

            db.Entry(cbnv).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CBNVExists(id))
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

        // POST: api/CBNV
        [ResponseType(typeof(CBNV))]
        public async Task<IHttpActionResult> PostCBNV(CBNV cbnv)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Generate the MaCBNV value
            cbnv.MaCBNV = GenerateMaCBNV();
            db.CBNVs.Add(cbnv);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (CBNVExists(cbnv.MaCBNV))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = cbnv.MaCBNV }, cbnv);
        }

        // DELETE: api/CBNV/5
        [ResponseType(typeof(CBNV))]
        public async Task<IHttpActionResult> DeleteCBNV(string id)
        {
            CBNV cbnv = await db.CBNVs.FindAsync(id);
            if (cbnv == null)
            {
                return NotFound();
            }

            db.CBNVs.Remove(cbnv);
            await db.SaveChangesAsync();

            return Ok(cbnv);
        }

        private string GenerateMaCBNV()
        {
            var prefix = "CBNV";
            var lastCBNV = db.CBNVs.OrderByDescending(c => c.MaCBNV).FirstOrDefault();

            if (lastCBNV == null)
            {
                return $"{prefix}00001";
            }

            int currentNumber = int.Parse(lastCBNV.MaCBNV.Substring(prefix.Length));
            string newNumber = (currentNumber + 1).ToString("D5"); // Pad the number with leading zeros to 5 digits

            return $"{prefix}{newNumber}";
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CBNVExists(string id)
        {
            return db.CBNVs.Count(e => e.MaCBNV == id) > 0;
        }
    }
}