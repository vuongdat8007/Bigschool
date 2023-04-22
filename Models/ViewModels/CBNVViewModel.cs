using Bigschool_TH_11.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Bigschool_TH_11.ViewModels
{
    public class CBNVViewModel
    {
        public CBNV CBNV { get; set; }
        public BankingInfo BankingInfo { get; set; }
        public List<ChuyenNganh> ChuyenNganhs { get; set; }
        public PhongBan PhongBan { get; set; }
    }
}