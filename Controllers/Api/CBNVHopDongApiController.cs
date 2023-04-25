using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Http;
using Bigschool_TH_11.Models;

namespace Bigschool_TH_11.Controllers.Api
{
    public class CBNVHopDongApiController : ApiController
    {
        private ApplicationDbContext _context;

        public CBNVHopDongApiController()
        {
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        // GET: api/CBNVHopDongApi
        public IHttpActionResult GetCBNVHopDongs()
        {
            var cbnvHopDongs = _context.HopDongCBNVs
                .Include(h => h.CBNV)
                .Include(h => h.HopDong)
                .ToList();

            return Ok(cbnvHopDongs);
        }

        // GET: api/CBNVHopDongApi/5
        public IHttpActionResult GetCBNVHopDong(int id)
        {
            var cbnvHopDong = _context.HopDongCBNVs
                .Include(h => h.CBNV)
                .Include(h => h.HopDong)
                .SingleOrDefault(h => h.ID == id);

            if (cbnvHopDong == null)
            {
                return NotFound();
            }

            return Ok(cbnvHopDong);
        }

        // POST: api/CBNVHopDongApi
        [HttpPost]
        public IHttpActionResult CreateCBNVHopDong(HopDongCBNV cbnvHopDong)
        {
            /*if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }*/ // Because CBNVHopDong.Id is auto-generated

            _context.HopDongCBNVs.Add(cbnvHopDong);
            _context.SaveChanges();

            return Created(new Uri(Request.RequestUri + "/" + cbnvHopDong.ID), cbnvHopDong);
        }

        // PUT: api/CBNVHopDongApi/5
        [HttpPut]
        public IHttpActionResult UpdateCBNVHopDong(int id, HopDongCBNV cbnvHopDong)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var cbnvHopDongInDb = _context.HopDongCBNVs.SingleOrDefault(h => h.ID == id);

            if (cbnvHopDongInDb == null)
            {
                return NotFound();
            }

            cbnvHopDongInDb.MaCBNV = cbnvHopDong.MaCBNV;
            cbnvHopDongInDb.MaHopDong = cbnvHopDong.MaHopDong;
            cbnvHopDongInDb.NgayKyHopDong = cbnvHopDong.NgayKyHopDong;
            cbnvHopDongInDb.NgayKetThucHopDong = cbnvHopDong.NgayKetThucHopDong;
            cbnvHopDongInDb.TinhTrangHopDong = cbnvHopDong.TinhTrangHopDong;
            cbnvHopDongInDb.GhiChu = cbnvHopDong.GhiChu;

            _context.SaveChanges();

            return Ok();
        }

        // DELETE: api/CBNVHopDongApi/5
        [HttpDelete]
        public IHttpActionResult DeleteCBNVHopDong(int id)
        {
            var cbnvHopDongInDb = _context.HopDongCBNVs.SingleOrDefault(h => h.ID == id);

            if (cbnvHopDongInDb == null)
            {
                return NotFound();
            }
            _context.HopDongCBNVs.Remove(cbnvHopDongInDb);
            _context.SaveChanges();

            return Ok();
        }
    }
}