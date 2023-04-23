using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Bigschool_TH_11.Controllers
{
    public class ThayDoiController : Controller
    {
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
    }
}
