using Bigschool_TH_11.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Bigschool_TH_11.ViewModels
{
    public class EditAssignedCBNVsViewModel
    {
        public ChucVu ChucVu { get; set; }
        public IEnumerable<CBNV> CBNVs { get; set; }
        public List<string> AssignedCBNVs { get; set; }
    }
}