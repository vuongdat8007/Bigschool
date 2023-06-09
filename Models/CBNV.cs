﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Bigschool_TH_11.Models
{
    public class CBNV
    {
        [Key]
        public string MaCBNV { get; set; }
        public string HoTen { get; set; }
        public DateTime NgaySinh { get; set; }
        public string NoiSinh { get; set; }
        public string QueQuan { get; set; }
        public string HoKhau { get; set; }
        public string DiaChi { get; set; }
        public string QuocTich { get; set; }
        public string DanToc { get; set; }
        public string TonGiao { get; set; }
        public string GioiTinh { get; set; }
        public string TrinhDoVanHoa { get; set; }
        public string DienThoai { get; set; }
        public string Email { get; set; }
        public DateTime NgayVaoTruong { get; set; }
        public int ThamNienCongTac { get; set; }
        public string SoCMND { get; set; }
        // Navigation property for the related ChuyenNganhs table
        public virtual ICollection<CBNVChuyenNganh> CBNVChuyenNganhs { get; set; }

        //public virtual ICollection<BankingInfo> BankingInfos { get; set; }
        public virtual BankingInfo BankingInfo { get; set; }

        public string MaChucVu { get; set; }
        public ChucVu ChucVu { get; set; }
        // Navigation property for PhongBan
        public string MaPhongBan { get; set; }
        [ForeignKey("MaPhongBan")]
        public virtual PhongBan PhongBan { get; set; }

        // Add a property to store the UserId as a foreign key
        public string ApplicationUserId { get; set; }

        // Add a navigation property for the ApplicationUser
        [ForeignKey("ApplicationUserId")]
        public virtual ApplicationUser ApplicationUser { get; set; }
    }
}