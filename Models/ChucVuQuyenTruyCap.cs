using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Bigschool_TH_11.Models
{
    public class ChucVuQuyenTruyCap
    {
        [Key, ForeignKey("ChucVu")]
        public string MaChucVu { get; set; }
        public ChucVu ChucVu { get; set; }

        [Key, ForeignKey("QuyenTruyCap")]
        public string MaQuyen { get; set; }
        public QuyenTruyCap QuyenTruyCap { get; set; }
    }
}