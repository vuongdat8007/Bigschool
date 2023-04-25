namespace Bigschool_TH_11.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ModifyHopDongCBNV_NgayKetThucNullable : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.HopDongCBNVs", "NgayKetThucHopDong", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.HopDongCBNVs", "NgayKetThucHopDong", c => c.DateTime(nullable: false));
        }
    }
}
