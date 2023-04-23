using Bigschool_TH_11.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Bigschool_TH_11.Controllers
{
    public class ThayDoiController : Controller
    {
        private ApplicationDbContext _context;

        public ThayDoiController() { 
            _context = new ApplicationDbContext();
        }
        // GET: ThayDoi
        public ActionResult Index()
        {
            return View();
        }

        // GET: ThayDoi/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: ThayDoi/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ThayDoi/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: ThayDoi/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: ThayDoi/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: ThayDoi/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ThayDoi/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        public ActionResult CreateEditThayDoi(int? id)
        {
            ThayDoi thayDoi;

            if (id == null)
            {
                thayDoi = new ThayDoi();
            }
            else
            {
                thayDoi = _context.ThayDois.SingleOrDefault(t => t.Id == id);
                if (thayDoi == null)
                {
                    return HttpNotFound();
                }
            }

            ViewBag.MaCBNV = new SelectList(_context.CBNVs, "MaCBNV", "HoTen");
            ViewBag.MaPhongBan = new SelectList(_context.PhongBans, "MaPhongBan", "TenPhongBan");
            ViewBag.MaChucVu = new SelectList(_context.ChucVus, "MaChucVu", "TenChucVu");

            return PartialView("_CreateEditThayDoiPartial", thayDoi);
        }
    }
}
