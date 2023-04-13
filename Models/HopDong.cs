using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Bigschool_TH_11.Models
{
    public class HopDong
    {
        [Key]
        public string MaHopDong { get; set; }
        public string LoaiHopDong { get; set; }
    }
}