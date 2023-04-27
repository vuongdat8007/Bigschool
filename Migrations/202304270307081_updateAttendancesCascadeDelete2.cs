namespace Bigschool_TH_11.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateAttendancesCascadeDelete2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Attendances", "Course_Id", c => c.Int());
            CreateIndex("dbo.Attendances", "Course_Id");
            AddForeignKey("dbo.Attendances", "Course_Id", "dbo.Courses", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Attendances", "Course_Id", "dbo.Courses");
            DropIndex("dbo.Attendances", new[] { "Course_Id" });
            DropColumn("dbo.Attendances", "Course_Id");
        }
    }
}
