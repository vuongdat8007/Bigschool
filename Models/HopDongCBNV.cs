using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Bigschool_TH_11.Models
{
    public class HopDongCBNV
    {
        [Key, ForeignKey("CBNV")]
        public string MaCBNV { get; set; }
        public CBNV CBNV { get; set; }

        [Key, ForeignKey("HopDong")]
        public string MaHopDong { get; set; }
        public HopDong HopDong { get; set; }
    }
}