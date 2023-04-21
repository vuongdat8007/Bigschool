using Bigschool_TH_11.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Bigschool_TH_11.ViewModels
{
    public class ChucNangEditViewModel
    {
        public ChucNang ChucNang { get; set; }
        public IEnumerable<ChucNang> ChucNangs { get; set; }
    }
}