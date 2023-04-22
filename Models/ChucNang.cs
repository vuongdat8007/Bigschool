using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Bigschool_TH_11.Models
{
    public class ChucNang
    {
        [Key]
        public string MaChucNang { get; set; }
        public string TenChucNang { get; set; }

        public ICollection<QuyenTruyCap> QuyenTruyCaps { get; set; }
    }
}