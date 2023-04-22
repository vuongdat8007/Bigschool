using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using Bigschool_TH_11.Models;

namespace Bigschool_TH_11.Controllers
{
    public class PhongBansApiController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/PhongBansApi
        public IQueryable<PhongBan> GetPhongBans()
        {
            return db.PhongBans;
        }

        // GET: api/PhongBansApi/5
        [ResponseType(typeof(PhongBan))]
        public async Task<IHttpActionResult> GetPhongBan(string id)
        {
            PhongBan phongBan = await db.PhongBans.FindAsync(id);
            if (phongBan == null)
            {
                return NotFound();
            }

            return Ok(phongBan);
        }

        // PUT: api/PhongBansApi/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutPhongBan(string id, PhongBan phongBan)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != phongBan.MaPhongBan)
            {
                return BadRequest();
            }

            db.Entry(phongBan).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PhongBanExists(id))
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

        // POST: api/PhongBansApi
        [ResponseType(typeof(PhongBan))]
        public async Task<IHttpActionResult> PostPhongBan(PhongBan phongBan)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.PhongBans.Add(phongBan);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (PhongBanExists(phongBan.MaPhongBan))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = phongBan.MaPhongBan }, phongBan);
        }

        // DELETE: api/PhongBansApi/5
        [ResponseType(typeof(PhongBan))]
        public async Task<IHttpActionResult> DeletePhongBan(string id)
        {
            PhongBan phongBan = await db.PhongBans.FindAsync(id);
            if (phongBan == null)
            {
                return NotFound();
            }

            db.PhongBans.Remove(phongBan);
            await db.SaveChangesAsync();

            return Ok(phongBan);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PhongBanExists(string id)
        {
            return db.PhongBans.Count(e => e.MaPhongBan == id) > 0;
        }
    }
}