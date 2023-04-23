namespace Bigschool_TH_11.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateThayDoiModel : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ThayDois", "MaCBNV", "dbo.CBNVs");
            DropPrimaryKey("dbo.ThayDois");
            AddColumn("dbo.ThayDois", "Id", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.ThayDois", "Id");
            AddForeignKey("dbo.ThayDois", "MaCBNV", "dbo.CBNVs", "MaCBNV", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ThayDois", "MaCBNV", "dbo.CBNVs");
            DropPrimaryKey("dbo.ThayDois");
            DropColumn("dbo.ThayDois", "Id");
            AddPrimaryKey("dbo.ThayDois", "MaCBNV");
            AddForeignKey("dbo.ThayDois", "MaCBNV", "dbo.CBNVs", "MaCBNV");
        }
    }
}
