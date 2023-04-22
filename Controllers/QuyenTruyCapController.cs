using Bigschool_TH_11.Models;
using Bigschool_TH_11.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Bigschool_TH_11.Controllers
{
    public class QuyenTruyCapController : Controller
    {
        private ApplicationDbContext _context;

        public QuyenTruyCapController()
        {
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        public ActionResult Create()
        {
            ViewBag.MaChucNang = new SelectList(_context.ChucNangs, "MaChucNang", "TenChucNang");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaQuyen,TenQuyen,MaChucNang")] QuyenTruyCap quyenTruyCap)
        {
            if (ModelState.IsValid)
            {
                quyenTruyCap.MaQuyen = Guid.NewGuid().ToString();
                try
                {
                    _context.QuyenTruyCaps.Add(quyenTruyCap);
                    _context.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch (DbEntityValidationException ex)
                {
                    foreach (var validationErrors in ex.EntityValidationErrors)
                    {
                        foreach (var validationError in validationErrors.ValidationErrors)
                        {
                            // You can log the validation error messages or show them in your view.
                            System.Diagnostics.Trace.TraceError("Property: {0} Error: {1}",
                                validationError.PropertyName,
                                validationError.ErrorMessage);
                        }
                    }
                }
            }

            ViewBag.MaChucNang = new SelectList(_context.ChucNangs, "MaChucNang", "TenChucNang", quyenTruyCap.MaChucNang);
            return View(quyenTruyCap);
        }

        public ActionResult Index()
        {
            var quyenTruyCaps = _context.QuyenTruyCaps.ToList();
            return View(quyenTruyCaps);
        }

        public ActionResult Edit(string id)
        {
            var item = _context.QuyenTruyCaps.SingleOrDefault(c => c.MaQuyen == id);

            if (item == null)
            {
                return HttpNotFound();
            }

            var viewModel = new QuyenTruyCapEditViewModel
            {
                QuyenTruyCap = item,
                QuyenTruyCaps = _context.QuyenTruyCaps.ToList()
            };

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(QuyenTruyCapEditViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                viewModel.QuyenTruyCaps = _context.QuyenTruyCaps.ToList();
                return View("Edit", viewModel);
            }

            var itemInDb = _context.QuyenTruyCaps.SingleOrDefault(c => c.MaQuyen == viewModel.QuyenTruyCap.MaQuyen);

            if (itemInDb == null)
            {
                return HttpNotFound();
            }

            itemInDb.TenQuyen = viewModel.QuyenTruyCap.TenQuyen;

            _context.SaveChanges();

            return RedirectToAction("Index", "QuyenTruyCap");
        }
    }
}