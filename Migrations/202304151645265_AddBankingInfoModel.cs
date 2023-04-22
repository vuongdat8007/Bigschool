namespace Bigschool_TH_11.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddBankingInfoModel : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.BankingInfoes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CBNVId = c.String(),
                        BankName = c.String(),
                        AccountNumber = c.String(),
                        AccountHolderName = c.String(),
                        Branch = c.String(),
                        SwiftCode = c.String(),
                        PaymentMethod = c.Int(nullable: false),
                        CBNV_MaCBNV = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.CBNVs", t => t.CBNV_MaCBNV)
                .Index(t => t.CBNV_MaCBNV);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.BankingInfoes", "CBNV_MaCBNV", "dbo.CBNVs");
            DropIndex("dbo.BankingInfoes", new[] { "CBNV_MaCBNV" });
            DropTable("dbo.BankingInfoes");
        }
    }
}
