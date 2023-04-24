using Bigschool_TH_11.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Http;

namespace Bigschool_TH_11.Controllers.Api
{
    public class ThayDoiApiController : ApiController
    {
        private ApplicationDbContext _context;

        public ThayDoiApiController()
        {
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        // GET: api/ThayDoiApi
        public IHttpActionResult GetThayDois()
        {
            var thayDois = _context.ThayDois
                .Include(t => t.CBNV)
                .Include(t => t.PhongBan)
                .Include(t => t.ChucVu)
                .ToList();

            return Ok(thayDois);
        }

        // GET: api/ThayDoiApi/5
        public IHttpActionResult GetThayDoi(int id)
        {
            var thayDoi = _context.ThayDois
                .Include(t => t.CBNV)
                .Include(t => t.PhongBan)
                .Include(t => t.ChucVu)
                .SingleOrDefault(t => t.Id == id);

            if (thayDoi == null)
            {
                return NotFound();
            }

            return Ok(thayDoi);
        }

        // POST: api/ThayDoiApi
        [HttpPost]
        public IHttpActionResult CreateThayDoi(ThayDoi thayDoi)
        {
            /*if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }*/ // Id is auto-generated in ThayDoi Model

            _context.ThayDois.Add(thayDoi);
            _context.SaveChanges();

            return Created(new Uri(Request.RequestUri + "/" + thayDoi.Id), thayDoi);
        }

        // PUT: api/ThayDoiApi/5
        [HttpPut]
        public IHttpActionResult UpdateThayDoi(int id, ThayDoi thayDoi)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var thayDoiInDb = _context.ThayDois.SingleOrDefault(t => t.Id == id);

            if (thayDoiInDb == null)
            {
                return NotFound();
            }

            thayDoiInDb.MaCBNV = thayDoi.MaCBNV;
            thayDoiInDb.MaPhongBan = thayDoi.MaPhongBan;
            thayDoiInDb.MaChucVu = thayDoi.MaChucVu;
            thayDoiInDb.NgayChuyen = thayDoi.NgayChuyen;
            thayDoiInDb.NoiDen = thayDoi.NoiDen;
            thayDoiInDb.LyDoChuyen = thayDoi.LyDoChuyen;

            _context.SaveChanges();

            return Ok();
        }

        // DELETE: api/ThayDoiApi/5
        [HttpDelete]
        public IHttpActionResult DeleteThayDoi(int id)
        {
            var thayDoiInDb = _context.ThayDois.SingleOrDefault(t => t.Id == id);
            
            if (thayDoiInDb == null)
            {
                return NotFound();
            }

            _context.ThayDois.Remove(thayDoiInDb);
            _context.SaveChanges();

            return Ok();
        }
    }
}