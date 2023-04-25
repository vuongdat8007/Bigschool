using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Bigschool_TH_11.Models;
using Bigschool_TH_11.ViewModels;

namespace Bigschool_TH_11.Controllers
{
    public class KhenThuongKyLuatsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: KhenThuongKyLuats
        public async Task<ActionResult> Index()
        {
            return View(await db.KhenThuongKyLuats.ToListAsync());
        }

        // GET: KhenThuongKyLuats/CBNVKhenThuongKyLuat
        public ActionResult CBNVKhenThuongKyLuat()
        {
            // Populate the ViewModel with the necessary data
            var viewModel = new CBNVKhenThuongKyLuatViewModel
            {
                CBNVList = db.CBNVs.ToList().Select(c => new SelectListItem
                {
                    Value = c.MaCBNV,
                    Text = c.HoTen // Replace 'FullName' with the appropriate property to display in the dropdown
                }),
                KhenThuongKyLuatList = db.KhenThuongKyLuats.ToList().Select(kt => new SelectListItem
                {
                    Value = kt.MaKTKL,
                    Text = kt.TenLyDo
                }),
            };

            return View(viewModel);
        }

        // GET: KhenThuongKyLuats/Details/5
        public async Task<ActionResult> Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            KhenThuongKyLuat khenThuongKyLuat = await db.KhenThuongKyLuats.FindAsync(id);
            if (khenThuongKyLuat == null)
            {
                return HttpNotFound();
            }
            return View(khenThuongKyLuat);
        }

        // GET: KhenThuongKyLuats/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: KhenThuongKyLuats/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "MaKTKL,TenLyDo")] KhenThuongKyLuat khenThuongKyLuat)
        {
            if (ModelState.IsValid)
            {
                db.KhenThuongKyLuats.Add(khenThuongKyLuat);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(khenThuongKyLuat);
        }

        // GET: KhenThuongKyLuats/Edit/5
        public async Task<ActionResult> Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            KhenThuongKyLuat khenThuongKyLuat = await db.KhenThuongKyLuats.FindAsync(id);
            if (khenThuongKyLuat == null)
            {
                return HttpNotFound();
            }
            return View(khenThuongKyLuat);
        }

        // POST: KhenThuongKyLuats/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "MaKTKL,TenLyDo")] KhenThuongKyLuat khenThuongKyLuat)
        {
            if (ModelState.IsValid)
            {
                db.Entry(khenThuongKyLuat).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(khenThuongKyLuat);
        }

        // GET: KhenThuongKyLuats/Delete/5
        public async Task<ActionResult> Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            KhenThuongKyLuat khenThuongKyLuat = await db.KhenThuongKyLuats.FindAsync(id);
            if (khenThuongKyLuat == null)
            {
                return HttpNotFound();
            }
            return View(khenThuongKyLuat);
        }

        // POST: KhenThuongKyLuats/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(string id)
        {
            KhenThuongKyLuat khenThuongKyLuat = await db.KhenThuongKyLuats.FindAsync(id);
            db.KhenThuongKyLuats.Remove(khenThuongKyLuat);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
