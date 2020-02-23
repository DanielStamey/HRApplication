namespace EmployeeManager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Departments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DepartmentName = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Employees",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FirstName = c.String(),
                        LastName = c.String(),
                        Address = c.String(),
                        EmailAddress = c.String(),
                        PreferredContactPhoneNumber = c.String(),
                        PositionId = c.Int(nullable: false),
                        DepartmentId = c.Int(nullable: false),
                        StartDate = c.DateTime(nullable: false),
                        EndDate = c.DateTime(nullable: false),
                        EmploymentStatusId = c.Int(nullable: false),
                        ShiftId = c.Int(nullable: false),
                        ManagerId = c.Int(nullable: false),
                        TeamMemberPhotoId = c.Int(nullable: false),
                        FavoriteColor = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Departments", t => t.DepartmentId, cascadeDelete: true)
                .ForeignKey("dbo.EmploymentStatus", t => t.EmploymentStatusId, cascadeDelete: true)
                .ForeignKey("dbo.Employees", t => t.ManagerId)
                .ForeignKey("dbo.Positions", t => t.PositionId, cascadeDelete: true)
                .ForeignKey("dbo.Shifts", t => t.ShiftId, cascadeDelete: true)
                .ForeignKey("dbo.TeamMemberPhotoes", t => t.TeamMemberPhotoId, cascadeDelete: true)
                .Index(t => t.PositionId)
                .Index(t => t.DepartmentId)
                .Index(t => t.EmploymentStatusId)
                .Index(t => t.ShiftId)
                .Index(t => t.ManagerId)
                .Index(t => t.TeamMemberPhotoId);
            
            CreateTable(
                "dbo.EmploymentStatus",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Status = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Positions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PositionName = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Shifts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ShiftName = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.TeamMemberPhotoes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Employees", "TeamMemberPhotoId", "dbo.TeamMemberPhotoes");
            DropForeignKey("dbo.Employees", "ShiftId", "dbo.Shifts");
            DropForeignKey("dbo.Employees", "PositionId", "dbo.Positions");
            DropForeignKey("dbo.Employees", "ManagerId", "dbo.Employees");
            DropForeignKey("dbo.Employees", "EmploymentStatusId", "dbo.EmploymentStatus");
            DropForeignKey("dbo.Employees", "DepartmentId", "dbo.Departments");
            DropIndex("dbo.Employees", new[] { "TeamMemberPhotoId" });
            DropIndex("dbo.Employees", new[] { "ManagerId" });
            DropIndex("dbo.Employees", new[] { "ShiftId" });
            DropIndex("dbo.Employees", new[] { "EmploymentStatusId" });
            DropIndex("dbo.Employees", new[] { "DepartmentId" });
            DropIndex("dbo.Employees", new[] { "PositionId" });
            DropTable("dbo.TeamMemberPhotoes");
            DropTable("dbo.Shifts");
            DropTable("dbo.Positions");
            DropTable("dbo.EmploymentStatus");
            DropTable("dbo.Employees");
            DropTable("dbo.Departments");
        }
    }
}
