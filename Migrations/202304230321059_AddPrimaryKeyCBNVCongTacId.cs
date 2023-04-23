namespace Bigschool_TH_11.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddPrimaryKeyCBNVCongTacId : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CBNVCongTacs", "CBNVCongTacId", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.CBNVCongTacs", "CBNVCongTacId");
        }
    }
}
