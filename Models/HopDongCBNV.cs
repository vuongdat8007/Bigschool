﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Bigschool_TH_11.Models
{
    public class HopDongCBNV
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        [ForeignKey("CBNV")]
        public string MaCBNV { get; set; }
        public CBNV CBNV { get; set; }

        [ForeignKey("HopDong")]
        public string MaHopDong { get; set; }
        public HopDong HopDong { get; set; }
        public DateTime NgayKyHopDong { get; set; }
        public DateTime? NgayKetThucHopDong { get; set; }
        public string TinhTrangHopDong { get; set; }
        public string GhiChu { get; set; }
    }
}