using Bigschool_TH_11.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Bigschool_TH_11.ViewModels
{
    public class PhongBanViewModel
    {
        public PhongBan PhongBan { get; set; }
        public IEnumerable<CBNV> CBNVs { get; set; }
        public IEnumerable<PhongBan> PhongBans { get; set; }
        
    }
}