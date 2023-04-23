using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Bigschool_TH_11.Controllers
{
    [RoutePrefix("CBNVCongTac")]
    public class CBNVCongTacController : Controller
    {
        // GET: CBNVCongTac/Manage
        [Route("Manage")]
        public ActionResult Manage()
        {
            return View();
        }
    }
}