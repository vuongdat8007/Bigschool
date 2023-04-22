namespace Bigschool_TH_11.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddBangCapCBNVChuyenNganhModel : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.BangCapCBNVChuyenNganhs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CBNVId = c.String(nullable: false, maxLength: 128),
                        ChuyenNganhId = c.String(nullable: false, maxLength: 128),
                        TenBangCap = c.String(),
                        NgayCap = c.DateTime(nullable: false),
                        TenTruong = c.String(),
                        LoaiBang = c.String(),
                        NamTotNghiep = c.DateTime(nullable: false),
                        HinhThucDaoTao = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.CBNVs", t => t.CBNVId, cascadeDelete: true)
                .ForeignKey("dbo.ChuyenNganhs", t => t.ChuyenNganhId, cascadeDelete: true)
                .Index(t => t.CBNVId)
                .Index(t => t.ChuyenNganhId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.BangCapCBNVChuyenNganhs", "ChuyenNganhId", "dbo.ChuyenNganhs");
            DropForeignKey("dbo.BangCapCBNVChuyenNganhs", "CBNVId", "dbo.CBNVs");
            DropIndex("dbo.BangCapCBNVChuyenNganhs", new[] { "ChuyenNganhId" });
            DropIndex("dbo.BangCapCBNVChuyenNganhs", new[] { "CBNVId" });
            DropTable("dbo.BangCapCBNVChuyenNganhs");
        }
    }
}
