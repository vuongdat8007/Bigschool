using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Bigschool_TH_11.Models
{
    [AtLeastOneNotNull("MaPhongBan", "MaChucVu", ErrorMessage = "Either MaPhongBan or MaChucVu must have a non-null value.")]
    public class ThayDoi
    {
        [Key]
        public int Id { get; set; }
        [Required]

        [ForeignKey("CBNV")]
        public string MaCBNV { get; set; }
        public CBNV CBNV { get; set; }

        [ForeignKey("PhongBan")]
        public string MaPhongBan { get; set; }
        public PhongBan PhongBan { get; set; }

        [ForeignKey("ChucVu")]
        public string MaChucVu { get; set; }
        public ChucVu ChucVu { get; set; }

        public DateTime NgayChuyen { get; set; }
        public string NoiDen { get; set; }
        public string LyDoChuyen { get; set; }
    }
}