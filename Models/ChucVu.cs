using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Bigschool_TH_11.Models
{
    public class ChucVu
    {
        [Key]
        public string MaChucVu { get; set; }
        public string TenChucVu { get; set; }

        public ICollection<CBNV> CBNVs { get; set; }

        public string MaQuyen { get; set; }
        public QuyenTruyCap QuyenTruyCap { get; set; }
    }
}