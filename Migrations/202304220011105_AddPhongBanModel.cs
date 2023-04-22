namespace Bigschool_TH_11.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddPhongBanModel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CBNVs", "MaPhongBan", c => c.String(maxLength: 128));
            CreateIndex("dbo.CBNVs", "MaPhongBan");
            AddForeignKey("dbo.CBNVs", "MaPhongBan", "dbo.PhongBans", "MaPhongBan");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CBNVs", "MaPhongBan", "dbo.PhongBans");
            DropIndex("dbo.CBNVs", new[] { "MaPhongBan" });
            DropColumn("dbo.CBNVs", "MaPhongBan");
        }
    }
}
