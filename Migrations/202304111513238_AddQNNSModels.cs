namespace Bigschool_TH_11.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddQNNSModels : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CBNVChuyenNganhs",
                c => new
                    {
                        MaCBNV = c.String(nullable: false, maxLength: 128),
                        MaChuyenNganh = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.MaCBNV, t.MaChuyenNganh })
                .ForeignKey("dbo.CBNVs", t => t.MaCBNV, cascadeDelete: true)
                .ForeignKey("dbo.ChuyenNganhs", t => t.MaChuyenNganh, cascadeDelete: true)
                .Index(t => t.MaCBNV)
                .Index(t => t.MaChuyenNganh);
            
            CreateTable(
                "dbo.CBNVs",
                c => new
                    {
                        MaCBNV = c.String(nullable: false, maxLength: 128),
                        HoTen = c.String(),
                        NgaySinh = c.DateTime(nullable: false),
                        NoiSinh = c.String(),
                        QueQuan = c.String(),
                        HoKhau = c.String(),
                        DiaChi = c.String(),
                        QuocTich = c.String(),
                        DanToc = c.String(),
                        TonGiao = c.String(),
                        GioiTinh = c.String(),
                        TrinhDoVanHoa = c.String(),
                        DienThoai = c.String(),
                        Email = c.String(),
                        NgayVaoTruong = c.DateTime(nullable: false),
                        ThamNienCongTac = c.Int(nullable: false),
                        SoCMND = c.String(),
                    })
                .PrimaryKey(t => t.MaCBNV);
            
            CreateTable(
                "dbo.ChuyenNganhs",
                c => new
                    {
                        MaChuyenNganh = c.String(nullable: false, maxLength: 128),
                        TenChuyenNganh = c.String(),
                    })
                .PrimaryKey(t => t.MaChuyenNganh);
            
            CreateTable(
                "dbo.CBNVCongTacs",
                c => new
                    {
                        MaCBNV = c.String(nullable: false, maxLength: 128),
                        MaChucVu = c.String(nullable: false, maxLength: 128),
                        NgayBatDauCongTac = c.DateTime(nullable: false),
                        NgayKetThucCongTac = c.DateTime(nullable: false),
                        TenTruong = c.String(),
                        GhiChu = c.String(),
                    })
                .PrimaryKey(t => new { t.MaCBNV, t.MaChucVu })
                .ForeignKey("dbo.CBNVs", t => t.MaCBNV, cascadeDelete: true)
                .ForeignKey("dbo.ChucVus", t => t.MaChucVu, cascadeDelete: true)
                .Index(t => t.MaCBNV)
                .Index(t => t.MaChucVu);
            
            CreateTable(
                "dbo.ChucVus",
                c => new
                    {
                        MaChucVu = c.String(nullable: false, maxLength: 128),
                        TenChucVu = c.String(),
                        MaQuyen = c.String(),
                    })
                .PrimaryKey(t => t.MaChucVu);
            
            CreateTable(
                "dbo.ChucNangs",
                c => new
                    {
                        MaChucNang = c.String(nullable: false, maxLength: 128),
                        TenChucNang = c.String(),
                    })
                .PrimaryKey(t => t.MaChucNang);
            
            CreateTable(
                "dbo.ChucVuQuyenTruyCaps",
                c => new
                    {
                        MaChucVu = c.String(nullable: false, maxLength: 128),
                        MaQuyen = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.MaChucVu, t.MaQuyen })
                .ForeignKey("dbo.ChucVus", t => t.MaChucVu, cascadeDelete: true)
                .ForeignKey("dbo.QuyenTruyCaps", t => t.MaQuyen, cascadeDelete: true)
                .Index(t => t.MaChucVu)
                .Index(t => t.MaQuyen);
            
            CreateTable(
                "dbo.QuyenTruyCaps",
                c => new
                    {
                        MaQuyen = c.String(nullable: false, maxLength: 128),
                        TenQuyen = c.String(),
                        MaChucNang = c.String(),
                    })
                .PrimaryKey(t => t.MaQuyen);
            
            CreateTable(
                "dbo.HopDongCBNVs",
                c => new
                    {
                        MaCBNV = c.String(nullable: false, maxLength: 128),
                        MaHopDong = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.MaCBNV, t.MaHopDong })
                .ForeignKey("dbo.CBNVs", t => t.MaCBNV, cascadeDelete: true)
                .ForeignKey("dbo.HopDongs", t => t.MaHopDong, cascadeDelete: true)
                .Index(t => t.MaCBNV)
                .Index(t => t.MaHopDong);
            
            CreateTable(
                "dbo.HopDongs",
                c => new
                    {
                        MaHopDong = c.String(nullable: false, maxLength: 128),
                        LoaiHopDong = c.String(),
                    })
                .PrimaryKey(t => t.MaHopDong);
            
            CreateTable(
                "dbo.PhongBans",
                c => new
                    {
                        MaPhongBan = c.String(nullable: false, maxLength: 128),
                        TenPhongBan = c.String(),
                        SoDienThoai = c.String(),
                    })
                .PrimaryKey(t => t.MaPhongBan);
            
            CreateTable(
                "dbo.ThayDois",
                c => new
                    {
                        MaCBNV = c.String(nullable: false, maxLength: 128),
                        MaPhongBan = c.String(maxLength: 128),
                        MaChucVu = c.String(maxLength: 128),
                        NgayChuyen = c.DateTime(nullable: false),
                        NoiDen = c.String(),
                        LyDoChuyen = c.String(),
                    })
                .PrimaryKey(t => t.MaCBNV)
                .ForeignKey("dbo.CBNVs", t => t.MaCBNV)
                .ForeignKey("dbo.ChucVus", t => t.MaChucVu)
                .ForeignKey("dbo.PhongBans", t => t.MaPhongBan)
                .Index(t => t.MaCBNV)
                .Index(t => t.MaPhongBan)
                .Index(t => t.MaChucVu);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ThayDois", "MaPhongBan", "dbo.PhongBans");
            DropForeignKey("dbo.ThayDois", "MaChucVu", "dbo.ChucVus");
            DropForeignKey("dbo.ThayDois", "MaCBNV", "dbo.CBNVs");
            DropForeignKey("dbo.HopDongCBNVs", "MaHopDong", "dbo.HopDongs");
            DropForeignKey("dbo.HopDongCBNVs", "MaCBNV", "dbo.CBNVs");
            DropForeignKey("dbo.ChucVuQuyenTruyCaps", "MaQuyen", "dbo.QuyenTruyCaps");
            DropForeignKey("dbo.ChucVuQuyenTruyCaps", "MaChucVu", "dbo.ChucVus");
            DropForeignKey("dbo.CBNVCongTacs", "MaChucVu", "dbo.ChucVus");
            DropForeignKey("dbo.CBNVCongTacs", "MaCBNV", "dbo.CBNVs");
            DropForeignKey("dbo.CBNVChuyenNganhs", "MaChuyenNganh", "dbo.ChuyenNganhs");
            DropForeignKey("dbo.CBNVChuyenNganhs", "MaCBNV", "dbo.CBNVs");
            DropIndex("dbo.ThayDois", new[] { "MaChucVu" });
            DropIndex("dbo.ThayDois", new[] { "MaPhongBan" });
            DropIndex("dbo.ThayDois", new[] { "MaCBNV" });
            DropIndex("dbo.HopDongCBNVs", new[] { "MaHopDong" });
            DropIndex("dbo.HopDongCBNVs", new[] { "MaCBNV" });
            DropIndex("dbo.ChucVuQuyenTruyCaps", new[] { "MaQuyen" });
            DropIndex("dbo.ChucVuQuyenTruyCaps", new[] { "MaChucVu" });
            DropIndex("dbo.CBNVCongTacs", new[] { "MaChucVu" });
            DropIndex("dbo.CBNVCongTacs", new[] { "MaCBNV" });
            DropIndex("dbo.CBNVChuyenNganhs", new[] { "MaChuyenNganh" });
            DropIndex("dbo.CBNVChuyenNganhs", new[] { "MaCBNV" });
            DropTable("dbo.ThayDois");
            DropTable("dbo.PhongBans");
            DropTable("dbo.HopDongs");
            DropTable("dbo.HopDongCBNVs");
            DropTable("dbo.QuyenTruyCaps");
            DropTable("dbo.ChucVuQuyenTruyCaps");
            DropTable("dbo.ChucNangs");
            DropTable("dbo.ChucVus");
            DropTable("dbo.CBNVCongTacs");
            DropTable("dbo.ChuyenNganhs");
            DropTable("dbo.CBNVs");
            DropTable("dbo.CBNVChuyenNganhs");
        }
    }
}
