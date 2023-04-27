using Bigschool_TH_11.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using Bigschool_TH_11.ViewModels;

namespace Bigschool_TH_11.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext _dbContext;
        
        public HomeController()
        {
            _dbContext = new ApplicationDbContext();
        }


        public ActionResult Courses()
        {
            var upcomingCourses = _dbContext.Courses
                .Include(c => c.Lecturer)
                .Include(c => c.Category)
                .Where(c => c.DateTime > DateTime.Now);

            var viewModel = new CoursesViewModel
            {
                UpcomingCourses = upcomingCourses,
                ShowAction = User.Identity.IsAuthenticated
            };

            return View(viewModel);
        }

        public ActionResult Index()
        {
            return View();
        }
        public ActionResult About()
        {
            ViewBag.Message = "Bigschool - Phần Mềm Quản Lý Trường Tiểu Học";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Search(string query)
        {
            var viewModel = new SearchViewModel();

            if (!string.IsNullOrEmpty(query))
            {
                viewModel.Users = _dbContext.Users
                    .Where(u => u.UserName.Contains(query) || u.Email.Contains(query))
                    .ToList();

                viewModel.CBNVs = _dbContext.CBNVs
                    .Where(c => c.HoTen.Contains(query) || c.MaCBNV.Contains(query))
                    .ToList();

                viewModel.Roles = _dbContext.Roles
                    .Where(r => r.Name.Contains(query))
                    .ToList();

                viewModel.CBNVCongTacs = _dbContext.CBNVCongTacs
                    .Where(r => r.TenTruong.Contains(query))
                    .ToList();

                viewModel.BangCapCBNVChuyenNganhs = _dbContext.BangCapCBNVChuyenNganhs
                    .Where(r => r.TenTruong.Contains(query))
                    .ToList();
            }

            return View(viewModel);
        }
    }
}