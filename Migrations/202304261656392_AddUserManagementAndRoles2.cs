namespace Bigschool_TH_11.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddUserManagementAndRoles2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "CBNVId", c => c.String(maxLength: 128));
            CreateIndex("dbo.AspNetUsers", "CBNVId");
            AddForeignKey("dbo.AspNetUsers", "CBNVId", "dbo.CBNVs", "MaCBNV");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUsers", "CBNVId", "dbo.CBNVs");
            DropIndex("dbo.AspNetUsers", new[] { "CBNVId" });
            DropColumn("dbo.AspNetUsers", "CBNVId");
        }
    }
}
