using Microsoft.AspNet.Identity.EntityFramework;
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
        public DbSet<HopDong> HopDongs { get; set; }
        public DbSet<ChuyenNganh> ChuyenNganhs { get; set; }
        public DbSet<PhongBan> PhongBans { get; set; }
        public DbSet<ChucVu> ChucVus { get; set; }
        public DbSet<CBNVCongTac> CBNVCongTacs { get; set; }
        public DbSet<ThayDoi> ThayDois { get; set; }
        public DbSet<QuyenTruyCap> QuyenTruyCaps { get; set; }
        public DbSet<ChucNang> ChucNangs { get; set; }
        public DbSet<CBNVChuyenNganh> CBNVChuyenNganhs { get; set; }
        public DbSet<HopDongCBNV> HopDongCBNVs { get; set; }
        public DbSet<ChucVuQuyenTruyCap> ChucVuQuyenTruyCaps { get; set; }

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
            modelBuilder.Entity<Attendance>()
                .HasRequired(a => a.Course)
                .WithMany()
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ApplicationUser>()
                .HasMany(u => u.Followers)
                .WithRequired(f => f.Followee)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ApplicationUser>()
                .HasMany(u => u.Followees)
                .WithRequired(f => f.Follower)
                .WillCascadeOnDelete(false);

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