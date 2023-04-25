using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Bigschool_TH_11.Models
{
    public class KhenThuongKyLuat
    {
        [Key]
        public String MaKTKL { get; set; }
        public String TenLyDo { get; set; }
    }
}