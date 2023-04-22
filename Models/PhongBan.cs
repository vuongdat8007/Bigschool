using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Bigschool_TH_11.Models
{
    public class PhongBan
    {
        [Key]
        public string MaPhongBan { get; set; }
        public string TenPhongBan { get; set; }
        public string SoDienThoai { get; set; }

        // Navigation property for the one-to-many relationship with CBNV
        public ICollection<CBNV> CBNVs { get; set; }
    }
}