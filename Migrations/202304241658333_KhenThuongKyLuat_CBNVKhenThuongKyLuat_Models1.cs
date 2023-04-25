namespace Bigschool_TH_11.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class KhenThuongKyLuat_CBNVKhenThuongKyLuat_Models1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CBNVKhenThuongKyLuats",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        MaCBNV = c.String(maxLength: 128),
                        MaKTKL = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.CBNVs", t => t.MaCBNV)
                .ForeignKey("dbo.KhenThuongKyLuats", t => t.MaKTKL)
                .Index(t => t.MaCBNV)
                .Index(t => t.MaKTKL);
            
            CreateTable(
                "dbo.KhenThuongKyLuats",
                c => new
                    {
                        MaKTKL = c.String(nullable: false, maxLength: 128),
                        TenLyDo = c.String(),
                    })
                .PrimaryKey(t => t.MaKTKL);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CBNVKhenThuongKyLuats", "MaKTKL", "dbo.KhenThuongKyLuats");
            DropForeignKey("dbo.CBNVKhenThuongKyLuats", "MaCBNV", "dbo.CBNVs");
            DropIndex("dbo.CBNVKhenThuongKyLuats", new[] { "MaKTKL" });
            DropIndex("dbo.CBNVKhenThuongKyLuats", new[] { "MaCBNV" });
            DropTable("dbo.KhenThuongKyLuats");
            DropTable("dbo.CBNVKhenThuongKyLuats");
        }
    }
}
