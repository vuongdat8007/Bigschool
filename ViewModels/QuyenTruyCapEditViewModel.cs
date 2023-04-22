using Bigschool_TH_11.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Bigschool_TH_11.ViewModels
{
    public class QuyenTruyCapEditViewModel
    {
        public QuyenTruyCap QuyenTruyCap { get; set; }
        public IEnumerable<QuyenTruyCap> QuyenTruyCaps { get; set; }
    }
}