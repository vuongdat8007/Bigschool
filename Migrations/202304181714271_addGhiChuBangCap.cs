namespace Bigschool_TH_11.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addGhiChuBangCap : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.BangCapCBNVChuyenNganhs", "GhiChu", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.BangCapCBNVChuyenNganhs", "GhiChu");
        }
    }
}
