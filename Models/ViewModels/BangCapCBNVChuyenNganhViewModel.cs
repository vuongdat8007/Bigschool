using Bigschool_TH_11.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Bigschool_TH_11.ViewModels
{
    public class BangCapCBNVChuyenNganhViewModel
    {
        public CBNV CBNV { get; set; }
        public List<BangCapCBNVChuyenNganh> BangCapCBNVChuyenNganhs { get; set; }
        public List<ChuyenNganh> ChuyenNganhs { get; set; }
    }
}