﻿using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Bigschool_TH_11.Models
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Course> Courses { get; set; }
        public DbSet<Category> Categories { get; set; }

        public DbSet<Attendance> Attendances { get; set; }
        public DbSet<Following> Followings { get; set; }

        public DbSet<CBNV> CBNVs { get; set; }
        public DbSet<BankingInfo> BankingInfos { get; set; }
        public DbSet<HopDong> HopDongs { get; set; }
        public DbSet<ChuyenNganh> ChuyenNganhs { get; set; }
        public DbSet<PhongBan> PhongBans { get; set; }
        public DbSet<ChucVu> ChucVus { get; set; }
        public DbSet<CBNVCongTac> CBNVCongTacs { get; set; }
        public DbSet<ThayDoi> ThayDois { get; set; }
        public DbSet<QuyenTruyCap> QuyenTruyCaps { get; set; }
        public DbSet<ChucNang> ChucNangs { get; set; }
        public DbSet<CBNVChuyenNganh> CBNVChuyenNganhs { get; set; }

        public DbSet<BangCapCBNVChuyenNganh> BangCapCBNVChuyenNganhs { get; set; }
        public DbSet<HopDongCBNV> HopDongCBNVs { get; set; }
        public DbSet<ChucVuQuyenTruyCap> ChucVuQuyenTruyCaps { get; set; }
        public DbSet<CBNVKhenThuongKyLuat> CBNVKhenThuongKyLuats { get; set; }
        public DbSet<KhenThuongKyLuat> KhenThuongKyLuats { get; set; }

        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            /*modelBuilder.Entity<CBNV>()
                .HasOptional(c => c.BankingInfo) // The relationship is optional
                .WithRequired(b => b.CBNV) // Each BankingInfo must have a CBNV
                .WillCascadeOnDelete(true); // Enable cascade delete*/

            modelBuilder.Entity<Attendance>()
            .HasRequired(a => a.Course)
            .WithMany()
            .HasForeignKey(a => a.CourseId)
            .WillCascadeOnDelete(false);

            modelBuilder.Entity<ApplicationUser>()
                .HasMany(u => u.Followers)
                .WithRequired(f => f.Followee)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ApplicationUser>()
                .HasMany(u => u.Followees)
                .WithRequired(f => f.Follower)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ApplicationUser>()
                .HasOptional(u => u.CBNV) // Make the relationship optional
                .WithMany() // Do not specify the navigation property on the other side
                .HasForeignKey(u => u.CBNVId); // Use CBNVId as the foreign key

            modelBuilder.Entity<CBNVCongTac>()
                .HasKey(c => new { c.MaCBNV, c.MaChucVu });

            modelBuilder.Entity<CBNVChuyenNganh>()
                .HasKey(c => new { c.MaCBNV, c.MaChuyenNganh });

            modelBuilder.Entity<HopDongCBNV>()
                .HasKey(c => new { c.MaCBNV, c.MaHopDong });

            modelBuilder.Entity<ChucVuQuyenTruyCap>()
                .HasKey(c => new { c.MaChucVu, c.MaQuyen });

            base.OnModelCreating(modelBuilder);
        }
    }
}