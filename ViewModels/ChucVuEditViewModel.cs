using Bigschool_TH_11.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Bigschool_TH_11.ViewModels
{
    public class ChucVuEditViewModel
    {
        public ChucVu ChucVu { get; set; }
        public IEnumerable<ChucVu> ChucVus { get; set; }
    }
}