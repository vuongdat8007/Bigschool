using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Bigschool_TH_11.Models
{
    public class CBNVCongTac
    {
        [Key]
        public int CBNVCongTacId { get; set; }

        [ForeignKey("CBNV")]
        public string MaCBNV { get; set; }
        public CBNV CBNV { get; set; }

        [Key, ForeignKey("ChucVu")]
        public string MaChucVu { get; set; }
        public ChucVu ChucVu { get; set; }

        public DateTime NgayBatDauCongTac { get; set; }
        public DateTime NgayKetThucCongTac { get; set; }
        public string TenTruong { get; set; }
        public string GhiChu { get; set; }
    }
}