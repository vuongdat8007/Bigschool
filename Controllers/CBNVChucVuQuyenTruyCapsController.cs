using Bigschool_TH_11.Models;
using Bigschool_TH_11.ViewModels;

using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;

namespace Bigschool_TH_11.Controllers
{
    public class CBNVChucVuQuyenTruyCapsController : Controller
    {
        private ApplicationDbContext _context;

        public CBNVChucVuQuyenTruyCapsController()
        {
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        public ActionResult Edit(string id)
        {
            var cbnv = _context.CBNVs.Include(c => c.ChucVu).Include(c => c.ChucVu.QuyenTruyCap).Include(c => c.ChucVu.QuyenTruyCap.ChucNang).SingleOrDefault(c => c.MaCBNV == id);

            if (cbnv == null)
            {
                return HttpNotFound();
            }

            var viewModel = new CBNVChucVuQuyenTruyCapViewModel
            {
                CBNV = cbnv,
                ChucVu = cbnv.ChucVu,
                QuyenTruyCap = cbnv.ChucVu?.QuyenTruyCap,
                ChucNang = cbnv.ChucVu?.QuyenTruyCap?.ChucNang,
                ChucVus = _context.ChucVus.ToList(),
                QuyenTruyCaps = _context.QuyenTruyCaps.ToList(),
                ChucNangs = _context.ChucNangs.ToList()
            };

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(CBNVChucVuQuyenTruyCapViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                viewModel.ChucVus = _context.ChucVus.ToList();
                viewModel.QuyenTruyCaps = _context.QuyenTruyCaps.ToList();
                viewModel.ChucNangs = _context.ChucNangs.ToList();
                return View("Edit", viewModel);
            }

            var cbnvInDb = _context.CBNVs.Include(c => c.ChucVu).Include(c => c.ChucVu.QuyenTruyCap).Include(c => c.ChucVu.QuyenTruyCap.ChucNang).SingleOrDefault(c => c.MaCBNV == viewModel.CBNV.MaCBNV);

            if (cbnvInDb == null)
            {
                return HttpNotFound();
            }

            cbnvInDb.ChucVu = viewModel.ChucVu;
            cbnvInDb.ChucVu.QuyenTruyCap = viewModel.QuyenTruyCap;
            cbnvInDb.ChucVu.QuyenTruyCap.ChucNang = viewModel.ChucNang;

            _context.SaveChanges();

            return RedirectToAction("Index", "CBNV");
        }

        public ActionResult Index()
        {
            var cbnvChucVuQuyenTruyCaps = _context.CBNVs.Include(c => c.ChucVu).Include(c => c.ChucVu.QuyenTruyCap).Include(c => c.ChucVu.QuyenTruyCap.ChucNang).ToList();
            return View(cbnvChucVuQuyenTruyCaps);
        }

    }
}