namespace Bigschool_TH_11.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateCBNVKhenThuongKyLuat : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CBNVKhenThuongKyLuats", "NgayKhenThuongKyLuat", c => c.DateTime(nullable: false));
            AddColumn("dbo.CBNVKhenThuongKyLuats", "GhiChu", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.CBNVKhenThuongKyLuats", "GhiChu");
            DropColumn("dbo.CBNVKhenThuongKyLuats", "NgayKhenThuongKyLuat");
        }
    }
}
