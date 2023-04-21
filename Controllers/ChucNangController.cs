using Bigschool_TH_11.Models;
using Bigschool_TH_11.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Bigschool_TH_11.Controllers
{
    public class ChucNangController : Controller
    {
        private ApplicationDbContext _context;

        public ChucNangController()
        {
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        public ActionResult Index()
        {
            var chucNangs = _context.ChucNangs.ToList();
            return View(chucNangs);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ChucNang chucNang)
        {
            if (!ModelState.IsValid)
            {
                return View(chucNang);
            }

            _context.ChucNangs.Add(chucNang);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        public ActionResult Edit(string id)
        {
            var item = _context.ChucNangs.SingleOrDefault(c => c.MaChucNang == id);

            if (item == null)
            {
                return HttpNotFound();
            }

            var viewModel = new ChucNangEditViewModel
            {
                ChucNang = item,
                ChucNangs = _context.ChucNangs.ToList()
            };

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(ChucNangEditViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                viewModel.ChucNangs = _context.ChucNangs.ToList();
                return View("Edit", viewModel);
            }

            var itemInDb = _context.ChucNangs.SingleOrDefault(c => c.MaChucNang == viewModel.ChucNang.MaChucNang);

            if (itemInDb == null)
            {
                return HttpNotFound();
            }

            itemInDb.TenChucNang = viewModel.ChucNang.TenChucNang;

            _context.SaveChanges();

            return RedirectToAction("Index", "ChucNang");
        }
    }
}