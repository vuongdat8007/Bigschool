using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Bigschool_TH_11.Models;

namespace Bigschool_TH_11.ViewModels
{
    public class ManagementViewModel
    {
        public IEnumerable<CBNV> CBNVs { get; set; }
        public IEnumerable<HopDong> HopDongs { get; set; }
        public IEnumerable<ChuyenNganh> ChuyenNganhs { get; set; }
        public IEnumerable<PhongBan> PhongBans { get; set; }
        public IEnumerable<ChucVu> ChucVus { get; set; }
        public IEnumerable<CBNVCongTac> CBNVCongTac { get;set; }
        public IEnumerable<ThayDoi> ThayDois { get; set; }
        public IEnumerable<QuyenTruyCap> QuyenTruyCaps { get; set; }
        public IEnumerable<ChucNang> ChucNangs { get; set; }
        public IEnumerable<CBNVChuyenNganh> CBNVChuyenNganhs { get; set; }
        public IEnumerable<HopDongCBNV> HopDongCBNV { get; set; }
        public IEnumerable<ChucVuQuyenTruyCap> ChucVuQuyenTruyCaps { get; set; }


    }
}