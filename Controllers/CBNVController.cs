using Bigschool_TH_11.Models;
using System.Linq;
using System.Web.Mvc;

namespace Bigschool_TH_11.Controllers 
{
    public class CBNVController : Controller
    {
        private ApplicationDbContext _context;

        public CBNVController()
        {
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        // GET: CBNV
        public ActionResult Index()
        {
            return View();
        }
    }
}