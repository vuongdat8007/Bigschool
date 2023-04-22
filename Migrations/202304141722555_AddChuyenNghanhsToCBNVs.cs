namespace Bigschool_TH_11.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddChuyenNghanhsToCBNVs : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ChuyenNganhs", "CBNV_MaCBNV", c => c.String(maxLength: 128));
            CreateIndex("dbo.ChuyenNganhs", "CBNV_MaCBNV");
            AddForeignKey("dbo.ChuyenNganhs", "CBNV_MaCBNV", "dbo.CBNVs", "MaCBNV");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ChuyenNganhs", "CBNV_MaCBNV", "dbo.CBNVs");
            DropIndex("dbo.ChuyenNganhs", new[] { "CBNV_MaCBNV" });
            DropColumn("dbo.ChuyenNganhs", "CBNV_MaCBNV");
        }
    }
}
