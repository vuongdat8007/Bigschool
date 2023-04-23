using Bigschool_TH_11.Models;
using Bigschool_TH_11.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Bigschool_TH_11.Controllers
{
    public class ChucVuController : Controller
    {
        private ApplicationDbContext _context;

        public ChucVuController()
        {
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        public ActionResult Index()
        {
            var chucVus = _context.ChucVus.ToList();
            return View(chucVus);
        }

        public ActionResult Create()
        {
            ViewBag.MaQuyen = new SelectList(_context.QuyenTruyCaps, "MaQuyen", "TenQuyen");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ChucVu chucVu)
        {
            if (!ModelState.IsValid)
            {
                return View(chucVu);
            }

            _context.ChucVus.Add(chucVu);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        public ActionResult Edit(string id)
        {
            var chucVu = _context.ChucVus.SingleOrDefault(c => c.MaChucVu == id);

            if (chucVu == null)
            {
                return HttpNotFound();
            }

            var viewModel = new ChucVuEditViewModel
            {
                ChucVu = chucVu,
                ChucVus = _context.ChucVus.ToList()
            };

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(ChucVuEditViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                viewModel.ChucVus = _context.ChucVus.ToList();
                return View("Edit", viewModel);
            }

            var chucVuInDb = _context.ChucVus.SingleOrDefault(c => c.MaChucVu == viewModel.ChucVu.MaChucVu);

            if (chucVuInDb == null)
            {
                return HttpNotFound();
            }

            chucVuInDb.TenChucVu = viewModel.ChucVu.TenChucVu;

            _context.SaveChanges();

            return RedirectToAction("Index", "ChucVu");
        }

        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            ChucVu chucVu = _context.ChucVus.Find(id);

            if (chucVu == null)
            {
                return HttpNotFound();
            }

            return View(chucVu);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            ChucVu chucVu = _context.ChucVus.Find(id);
            _context.ChucVus.Remove(chucVu);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult EditAssignedCBNVs(string id)
        {
            var chucVu = _context.ChucVus.SingleOrDefault(c => c.MaChucVu == id);

            if (chucVu == null)
            {
                return HttpNotFound();
            }

            var viewModel = new EditAssignedCBNVsViewModel
            {
                ChucVu = chucVu,
                CBNVs = _context.CBNVs.ToList()
            };

            if (viewModel.CBNVs == null)
            {
                viewModel.CBNVs = new List<CBNV>();
            }

            if (viewModel.ChucVu.CBNVs == null)
            {
                viewModel.ChucVu.CBNVs = new List<CBNV>();
            }

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UpdateAssignedCBNVs(string id, List<string> AssignedCBNVs)
        {
            var chucVu = _context.ChucVus.Include(c => c.CBNVs).SingleOrDefault(c => c.MaChucVu == id);

            if (chucVu == null)
            {
                return HttpNotFound();
            }

            // Clear current CBNV assignments
            chucVu.CBNVs.Clear();

            // Assign selected CBNVs
            if (AssignedCBNVs != null)
            {
                foreach (var maCBNV in AssignedCBNVs)
                {
                    var cbnv = _context.CBNVs.SingleOrDefault(c => c.MaCBNV == maCBNV);
                    if (cbnv != null)
                    {
                        chucVu.CBNVs.Add(cbnv);
                    }
                }
            }

            _context.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}