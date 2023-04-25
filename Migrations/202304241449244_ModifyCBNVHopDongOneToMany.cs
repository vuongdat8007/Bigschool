namespace Bigschool_TH_11.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ModifyCBNVHopDongOneToMany : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.HopDongCBNVs", "ID", c => c.Int(nullable: false, identity: true));
            AddColumn("dbo.HopDongCBNVs", "NgayKyHopDong", c => c.DateTime(nullable: false));
            AddColumn("dbo.HopDongCBNVs", "NgayKetThucHopDong", c => c.DateTime(nullable: false));
            AddColumn("dbo.HopDongCBNVs", "TinhTrangHopDong", c => c.String());
            AddColumn("dbo.HopDongCBNVs", "GhiChu", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.HopDongCBNVs", "GhiChu");
            DropColumn("dbo.HopDongCBNVs", "TinhTrangHopDong");
            DropColumn("dbo.HopDongCBNVs", "NgayKetThucHopDong");
            DropColumn("dbo.HopDongCBNVs", "NgayKyHopDong");
            DropColumn("dbo.HopDongCBNVs", "ID");
        }
    }
}
