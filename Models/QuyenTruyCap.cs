using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Bigschool_TH_11.Models
{
    public class QuyenTruyCap
    {
        [Key]
        public string MaQuyen { get; set; }
        public string TenQuyen { get; set; }

        public ICollection<ChucVu> ChucVus { get; set; }

        public string MaChucNang { get; set; }
        public ChucNang ChucNang { get; set; }
    }
}