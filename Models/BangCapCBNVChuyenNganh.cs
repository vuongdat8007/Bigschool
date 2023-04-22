using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Bigschool_TH_11.Models
{
    public class BangCapCBNVChuyenNganh
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [ForeignKey("CBNV")]
        public string CBNVId { get; set; }

        [Required]
        [ForeignKey("ChuyenNganh")]
        public string ChuyenNganhId { get; set; }

        // Add other fields related to the certificate information here, for example:
        public string TenBangCap { get; set; }
        public DateTime NgayCap { get; set; }
        public string TenTruong { get; set; }
        public string LoaiBang { get; set; }
        public DateTime NamTotNghiep { get; set; }
        public string HinhThucDaoTao { get; set; }
        public string GhiChu { get; set; }

        public virtual CBNV CBNV { get; set; }
        public virtual ChuyenNganh ChuyenNganh { get; set; }
    }
}