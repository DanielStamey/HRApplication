namespace EmployeeManager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial14 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Employees", "TeamMemberPhotoId", "dbo.TeamMemberPhotoes");
            DropIndex("dbo.Employees", new[] { "TeamMemberPhotoId" });
            AddColumn("dbo.Employees", "TeamMemberPhoto", c => c.Binary());
            DropColumn("dbo.Employees", "TeamMemberPhotoId");
        }
        
        public override void Down()
        {            
            AddColumn("dbo.Employees", "TeamMemberPhotoId", c => c.Int());
            DropColumn("dbo.Employees", "TeamMemberPhoto");
            CreateIndex("dbo.Employees", "TeamMemberPhotoId");
            AddForeignKey("dbo.Employees", "TeamMemberPhotoId", "dbo.TeamMemberPhotoes", "Id");
        }
    }
}
