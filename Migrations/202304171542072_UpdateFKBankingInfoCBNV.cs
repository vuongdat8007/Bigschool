namespace Bigschool_TH_11.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateFKBankingInfoCBNV : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.BankingInfoes", new[] { "CBNV_MaCBNV" });
            DropColumn("dbo.BankingInfoes", "CBNVId");
            RenameColumn(table: "dbo.BankingInfoes", name: "CBNV_MaCBNV", newName: "CBNVId");
            DropPrimaryKey("dbo.BankingInfoes");
            AlterColumn("dbo.BankingInfoes", "Id", c => c.Int(nullable: false));
            AlterColumn("dbo.BankingInfoes", "CBNVId", c => c.String(nullable: false, maxLength: 128));
            AddPrimaryKey("dbo.BankingInfoes", "CBNVId");
            CreateIndex("dbo.BankingInfoes", "CBNVId");
        }
        
        public override void Down()
        {
            DropIndex("dbo.BankingInfoes", new[] { "CBNVId" });
            DropPrimaryKey("dbo.BankingInfoes");
            AlterColumn("dbo.BankingInfoes", "CBNVId", c => c.String());
            AlterColumn("dbo.BankingInfoes", "Id", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.BankingInfoes", "Id");
            RenameColumn(table: "dbo.BankingInfoes", name: "CBNVId", newName: "CBNV_MaCBNV");
            AddColumn("dbo.BankingInfoes", "CBNVId", c => c.String());
            CreateIndex("dbo.BankingInfoes", "CBNV_MaCBNV");
        }
    }
}
