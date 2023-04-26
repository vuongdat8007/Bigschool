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
    public class PhongBansController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: PhongBans
        public async Task<ActionResult> Index()
        {
            return View(await db.PhongBans.ToListAsync());
        }

        // GET: PhongBans/Details/5
        public async Task<ActionResult> Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PhongBan phongBan = await db.PhongBans.FindAsync(id);
            if (phongBan == null)
            {
                return HttpNotFound();
            }
            return View(phongBan);
        }

        // GET: PhongBans/Create
        [Authorize(Roles = "SuperAdmin")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: PhongBans/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "SuperAdmin")]
        public async Task<ActionResult> Create([Bind(Include = "MaPhongBan,TenPhongBan,SoDienThoai")] PhongBan phongBan)
        {
            if (ModelState.IsValid)
            {
                db.PhongBans.Add(phongBan);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(phongBan);
        }

        // GET: PhongBans/Edit/5
        [Authorize(Roles = "SuperAdmin")]
        public async Task<ActionResult> Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PhongBan phongBan = await db.PhongBans.FindAsync(id);
            if (phongBan == null)
            {
                return HttpNotFound();
            }
            return View(phongBan);
        }

        // POST: PhongBans/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "SuperAdmin")]
        public async Task<ActionResult> Edit([Bind(Include = "MaPhongBan,TenPhongBan,SoDienThoai")] PhongBan phongBan)
        {
            if (ModelState.IsValid)
            {
                db.Entry(phongBan).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(phongBan);
        }

        // GET: PhongBans/Delete/5
        [Authorize(Roles = "SuperAdmin")]
        public async Task<ActionResult> Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PhongBan phongBan = await db.PhongBans.FindAsync(id);
            if (phongBan == null)
            {
                return HttpNotFound();
            }
            return View(phongBan);
        }

        // POST: PhongBans/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "SuperAdmin")]
        public async Task<ActionResult> DeleteConfirmed(string id)
        {
            PhongBan phongBan = await db.PhongBans.FindAsync(id);
            db.PhongBans.Remove(phongBan);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        public ActionResult AssignCBNVsToPhongBan()
        {
            var viewModel = new PhongBanViewModel
            {
                PhongBans = db.PhongBans.ToList(), // Replace this line with the actual method to fetch PhongBans from your data source
                CBNVs = db.CBNVs.ToList(), // Replace this line with the actual method to fetch CBNVs from your data source
                CBNVSelections = db.CBNVs.Select(c => new CBNVSelection { CBNV = c, IsSelected = false }).ToList() // Replace this line with the actual method to fetch CBNVSelections from your data source
            };

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AssignCBNVsToPhongBan(PhongBanViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var phongBan = db.PhongBans.Find(viewModel.PhongBan.MaPhongBan);
                if (phongBan != null)
                {
                    foreach (var cbnvSelection in viewModel.CBNVSelections)
                    {
                        if (cbnvSelection.IsSelected)
                        {
                            var existingCBNV = db.CBNVs.Find(cbnvSelection.CBNV.MaCBNV);
                            if (existingCBNV != null)
                            {
                                existingCBNV.MaPhongBan = phongBan.MaPhongBan;
                                db.Entry(existingCBNV).State = EntityState.Modified;
                            }
                        }
                    }
                    db.SaveChanges();
                }
                return RedirectToAction("AssignCBNVsToPhongBan");
            }
            // Initialize PhongBans, CBNVs, and CBNVSelections properties before returning the view
            viewModel.PhongBans = db.PhongBans.ToList(); // Replace with your method to fetch PhongBans from your data source
            viewModel.CBNVs = db.CBNVs.ToList(); // Replace with your method to fetch CBNVs from your data source
            viewModel.CBNVSelections = db.CBNVs.Select(c => new CBNVSelection { CBNV = c, IsSelected = false }).ToList(); // Replace with your method to fetch CBNVSelections from your data source
            return View(viewModel);
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
