namespace Bigschool_TH_11.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddUserManagementAndRoles : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CBNVs", "ApplicationUserId", c => c.String(maxLength: 128));
            CreateIndex("dbo.CBNVs", "ApplicationUserId");
            AddForeignKey("dbo.CBNVs", "ApplicationUserId", "dbo.AspNetUsers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CBNVs", "ApplicationUserId", "dbo.AspNetUsers");
            DropIndex("dbo.CBNVs", new[] { "ApplicationUserId" });
            DropColumn("dbo.CBNVs", "ApplicationUserId");
        }
    }
}
