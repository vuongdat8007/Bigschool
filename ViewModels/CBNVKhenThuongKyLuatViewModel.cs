using Bigschool_TH_11.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Bigschool_TH_11.ViewModels
{
    public class CBNVKhenThuongKyLuatViewModel
    {
        public IEnumerable<SelectListItem> CBNVList { get; set; }
        public IEnumerable<SelectListItem> KhenThuongKyLuatList { get; set; }
        public CBNVKhenThuongKyLuat CBNVKhenThuongKyLuat { get; set; }
        public string MaCBNV { get; set; }
        public string MaKTKL { get; set; }
        public DateTime NgayKhenThuongKyLuat { get; set; }
        public string GhiChu { get; set; }
    }
}