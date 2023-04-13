using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Bigschool_TH_11.Models
{
    public class ChuyenNganh
    {
        [Key]
        public string MaChuyenNganh { get; set; }
        public string TenChuyenNganh { get; set; }
    }
}