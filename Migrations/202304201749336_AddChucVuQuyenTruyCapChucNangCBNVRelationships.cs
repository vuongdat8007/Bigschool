namespace Bigschool_TH_11.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddChucVuQuyenTruyCapChucNangCBNVRelationships : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CBNVs", "MaChucVu", c => c.String(maxLength: 128));
            AlterColumn("dbo.ChucVus", "MaQuyen", c => c.String(maxLength: 128));
            AlterColumn("dbo.QuyenTruyCaps", "MaChucNang", c => c.String(maxLength: 128));
            CreateIndex("dbo.CBNVs", "MaChucVu");
            CreateIndex("dbo.ChucVus", "MaQuyen");
            CreateIndex("dbo.QuyenTruyCaps", "MaChucNang");
            AddForeignKey("dbo.CBNVs", "MaChucVu", "dbo.ChucVus", "MaChucVu");
            AddForeignKey("dbo.QuyenTruyCaps", "MaChucNang", "dbo.ChucNangs", "MaChucNang");
            AddForeignKey("dbo.ChucVus", "MaQuyen", "dbo.QuyenTruyCaps", "MaQuyen");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ChucVus", "MaQuyen", "dbo.QuyenTruyCaps");
            DropForeignKey("dbo.QuyenTruyCaps", "MaChucNang", "dbo.ChucNangs");
            DropForeignKey("dbo.CBNVs", "MaChucVu", "dbo.ChucVus");
            DropIndex("dbo.QuyenTruyCaps", new[] { "MaChucNang" });
            DropIndex("dbo.ChucVus", new[] { "MaQuyen" });
            DropIndex("dbo.CBNVs", new[] { "MaChucVu" });
            AlterColumn("dbo.QuyenTruyCaps", "MaChucNang", c => c.String());
            AlterColumn("dbo.ChucVus", "MaQuyen", c => c.String());
            DropColumn("dbo.CBNVs", "MaChucVu");
        }
    }
}
