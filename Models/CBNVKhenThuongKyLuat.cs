using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Bigschool_TH_11.Models
{
    public class CBNVKhenThuongKyLuat
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        [ForeignKey("CBNV")]
        public String MaCBNV { get; set; }
        public CBNV CBNV { get; set; }

        [ForeignKey("KhenThuongKyLuat")]
        public String MaKTKL { get; set; }
        public KhenThuongKyLuat KhenThuongKyLuat { get; set; }

        public DateTime NgayKhenThuongKyLuat { get; set; }
        public String GhiChu { get; set; }
    }
}