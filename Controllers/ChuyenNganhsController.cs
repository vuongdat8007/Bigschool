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

namespace Bigschool_TH_11.Controllers
{
    public class ChuyenNganhsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: ChuyenNganhs
        public async Task<ActionResult> Index()
        {
            return View(await db.ChuyenNganhs.ToListAsync());
        }

        // GET: ChuyenNganhs/Details/5
        public async Task<ActionResult> Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ChuyenNganh chuyenNganh = await db.ChuyenNganhs.FindAsync(id);
            if (chuyenNganh == null)
            {
                return HttpNotFound();
            }
            return View(chuyenNganh);
        }

        // GET: ChuyenNganhs/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ChuyenNganhs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "MaChuyenNganh,TenChuyenNganh")] ChuyenNganh chuyenNganh)
        {
            if (ModelState.IsValid)
            {
                db.ChuyenNganhs.Add(chuyenNganh);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(chuyenNganh);
        }

        // GET: ChuyenNganhs/Edit/5
        public async Task<ActionResult> Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ChuyenNganh chuyenNganh = await db.ChuyenNganhs.FindAsync(id);
            if (chuyenNganh == null)
            {
                return HttpNotFound();
            }
            return View(chuyenNganh);
        }

        // POST: ChuyenNganhs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "MaChuyenNganh,TenChuyenNganh")] ChuyenNganh chuyenNganh)
        {
            if (ModelState.IsValid)
            {
                db.Entry(chuyenNganh).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(chuyenNganh);
        }

        // GET: ChuyenNganhs/Delete/5
        public async Task<ActionResult> Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ChuyenNganh chuyenNganh = await db.ChuyenNganhs.FindAsync(id);
            if (chuyenNganh == null)
            {
                return HttpNotFound();
            }
            return View(chuyenNganh);
        }

        // POST: ChuyenNganhs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(string id)
        {
            ChuyenNganh chuyenNganh = await db.ChuyenNganhs.FindAsync(id);
            db.ChuyenNganhs.Remove(chuyenNganh);
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
