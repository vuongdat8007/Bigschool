namespace Bigschool_TH_11.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateCBNVBankingInfoRelationship : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.BankingInfoes", "CBNV_MaCBNV", "dbo.CBNVs");
            DropIndex("dbo.BankingInfoes", new[] { "CBNV_MaCBNV" });
            AlterColumn("dbo.BankingInfoes", "CBNV_MaCBNV", c => c.String(nullable: false, maxLength: 128));
            CreateIndex("dbo.BankingInfoes", "CBNV_MaCBNV");
            AddForeignKey("dbo.BankingInfoes", "CBNV_MaCBNV", "dbo.CBNVs", "MaCBNV", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.BankingInfoes", "CBNV_MaCBNV", "dbo.CBNVs");
            DropIndex("dbo.BankingInfoes", new[] { "CBNV_MaCBNV" });
            AlterColumn("dbo.BankingInfoes", "CBNV_MaCBNV", c => c.String(maxLength: 128));
            CreateIndex("dbo.BankingInfoes", "CBNV_MaCBNV");
            AddForeignKey("dbo.BankingInfoes", "CBNV_MaCBNV", "dbo.CBNVs", "MaCBNV");
        }
    }
}
