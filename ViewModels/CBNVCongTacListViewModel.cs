using Bigschool_TH_11.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Bigschool_TH_11.ViewModels
{
    public class CBNVCongTacListViewModel
    {
        public List<CBNVCongTac> CBNVCongTacs { get; set; }
        public List<CBNV> CBNVs { get; set; }
        public List<ChucVu> ChucVuAvails { get; set; }
    }
}