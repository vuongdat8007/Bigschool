using Bigschool_TH_11.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Http;

namespace Bigschool_TH_11.Controllers.Api
{
    public class CBNVKhenThuongKyLuatApiController : ApiController
    {
        private ApplicationDbContext _context;

        public CBNVKhenThuongKyLuatApiController()
        {
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        // GET: api/CBNVKhenThuongKyLuatApi
        public IHttpActionResult GetCBNVKhenThuongKyLuats()
        {
            var cbnvKhenThuongKyLuats = _context.CBNVKhenThuongKyLuats
                .Include(c => c.CBNV)
                .Include(c => c.KhenThuongKyLuat)
                .ToList();

            return Ok(cbnvKhenThuongKyLuats);
        }

        // GET: api/CBNVKhenThuongKyLuatApi/5
        public IHttpActionResult GetCBNVKhenThuongKyLuat(int id)
        {
            var cbnvKhenThuongKyLuat = _context.CBNVKhenThuongKyLuats
                .Include(c => c.CBNV)
                .Include(c => c.KhenThuongKyLuat)
                .SingleOrDefault(c => c.ID == id);

            if (cbnvKhenThuongKyLuat == null)
            {
                return NotFound();
            }

            return Ok(cbnvKhenThuongKyLuat);
        }

        // POST: api/CBNVKhenThuongKyLuatApi
        [HttpPost]
        public IHttpActionResult CreateCBNVKhenThuongKyLuat(CBNVKhenThuongKyLuat cbnvKhenThuongKyLuat)
        {
            /*if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }*/ // Id is auto-generated at Model level

            _context.CBNVKhenThuongKyLuats.Add(cbnvKhenThuongKyLuat);
            _context.SaveChanges();

            return Created(new Uri(Request.RequestUri + "/" + cbnvKhenThuongKyLuat.ID), cbnvKhenThuongKyLuat);
        }

        // PUT: api/CBNVKhenThuongKyLuatApi/5
        [HttpPut]
        public IHttpActionResult UpdateCBNVKhenThuongKyLuat(int id, CBNVKhenThuongKyLuat cbnvKhenThuongKyLuat)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var cbnvKhenThuongKyLuatInDb = _context.CBNVKhenThuongKyLuats.SingleOrDefault(c => c.ID == id);

            if (cbnvKhenThuongKyLuatInDb == null)
            {
                return NotFound();
            }

            cbnvKhenThuongKyLuatInDb.MaCBNV = cbnvKhenThuongKyLuat.MaCBNV;
            cbnvKhenThuongKyLuatInDb.MaKTKL = cbnvKhenThuongKyLuat.MaKTKL;
            cbnvKhenThuongKyLuatInDb.NgayKhenThuongKyLuat = cbnvKhenThuongKyLuat.NgayKhenThuongKyLuat;
            cbnvKhenThuongKyLuatInDb.GhiChu = cbnvKhenThuongKyLuat.GhiChu;

            _context.SaveChanges();

            return Ok();
        }

        // DELETE: api/CBNVKhenThuongKyLuatApi/5
        [HttpDelete]
        public IHttpActionResult DeleteCBNVKhenThuongKyLuat(int id)
        {
            var cbnvKhenThuongKyLuatInDb = _context.CBNVKhenThuongKyLuats.SingleOrDefault(c => c.ID == id);
            if (cbnvKhenThuongKyLuatInDb == null)
            {
                return NotFound();
            }

            _context.CBNVKhenThuongKyLuats.Remove(cbnvKhenThuongKyLuatInDb);
            _context.SaveChanges();

            return Ok();
        }
    }
}