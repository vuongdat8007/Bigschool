using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Bigschool_TH_11.Models;

namespace Bigschool_TH_11.ViewModels
{
    public class CBNVChucVuQuyenTruyCapViewModel
    {
        public CBNV CBNV { get; set; }
        public ChucVu ChucVu { get; set; }
        public QuyenTruyCap QuyenTruyCap { get; set; }
        public ChucNang ChucNang { get; set; }

        public IEnumerable<ChucVu> ChucVus { get; set; }
        public IEnumerable<QuyenTruyCap> QuyenTruyCaps { get; set; }
        public IEnumerable<ChucNang> ChucNangs { get; set; }
    }
}