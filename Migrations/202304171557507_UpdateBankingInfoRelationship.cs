namespace Bigschool_TH_11.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateBankingInfoRelationship : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.BankingInfoes", "CBNVId", "dbo.CBNVs");
            AddForeignKey("dbo.BankingInfoes", "CBNVId", "dbo.CBNVs", "MaCBNV");
            DropColumn("dbo.BankingInfoes", "Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.BankingInfoes", "Id", c => c.Int(nullable: false));
            DropForeignKey("dbo.BankingInfoes", "CBNVId", "dbo.CBNVs");
            AddForeignKey("dbo.BankingInfoes", "CBNVId", "dbo.CBNVs", "MaCBNV", cascadeDelete: true);
        }
    }
}
