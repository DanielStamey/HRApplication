namespace EmployeeManager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial1 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Employees", "DepartmentId", "dbo.Departments");
            DropForeignKey("dbo.Employees", "EmploymentStatusId", "dbo.EmploymentStatus");
            DropForeignKey("dbo.Employees", "PositionId", "dbo.Positions");
            DropForeignKey("dbo.Employees", "ShiftId", "dbo.Shifts");
            DropForeignKey("dbo.Employees", "TeamMemberPhotoId", "dbo.TeamMemberPhotoes");
            DropIndex("dbo.Employees", new[] { "PositionId" });
            DropIndex("dbo.Employees", new[] { "DepartmentId" });
            DropIndex("dbo.Employees", new[] { "EmploymentStatusId" });
            DropIndex("dbo.Employees", new[] { "ShiftId" });
            DropIndex("dbo.Employees", new[] { "ManagerId" });
            DropIndex("dbo.Employees", new[] { "TeamMemberPhotoId" });
            RenameColumn(table: "dbo.Employees", name: "DepartmentId", newName: "Department_Id");
            RenameColumn(table: "dbo.Employees", name: "EmploymentStatusId", newName: "EmploymentStatus_Id");
            RenameColumn(table: "dbo.Employees", name: "ManagerId", newName: "Manager_Id");
            RenameColumn(table: "dbo.Employees", name: "PositionId", newName: "Position_Id");
            RenameColumn(table: "dbo.Employees", name: "ShiftId", newName: "Shift_Id");
            RenameColumn(table: "dbo.Employees", name: "TeamMemberPhotoId", newName: "TeamMemberPhoto_Id");
            AlterColumn("dbo.Employees", "Position_Id", c => c.Int());
            AlterColumn("dbo.Employees", "Department_Id", c => c.Int());
            AlterColumn("dbo.Employees", "EmploymentStatus_Id", c => c.Int());
            AlterColumn("dbo.Employees", "Shift_Id", c => c.Int());
            AlterColumn("dbo.Employees", "Manager_Id", c => c.Int());
            AlterColumn("dbo.Employees", "TeamMemberPhoto_Id", c => c.Int());
            CreateIndex("dbo.Employees", "Department_Id");
            CreateIndex("dbo.Employees", "EmploymentStatus_Id");
            CreateIndex("dbo.Employees", "Manager_Id");
            CreateIndex("dbo.Employees", "Position_Id");
            CreateIndex("dbo.Employees", "Shift_Id");
            CreateIndex("dbo.Employees", "TeamMemberPhoto_Id");
            AddForeignKey("dbo.Employees", "Department_Id", "dbo.Departments", "Id");
            AddForeignKey("dbo.Employees", "EmploymentStatus_Id", "dbo.EmploymentStatus", "Id");
            AddForeignKey("dbo.Employees", "Position_Id", "dbo.Positions", "Id");
            AddForeignKey("dbo.Employees", "Shift_Id", "dbo.Shifts", "Id");
            AddForeignKey("dbo.Employees", "TeamMemberPhoto_Id", "dbo.TeamMemberPhotoes", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Employees", "TeamMemberPhoto_Id", "dbo.TeamMemberPhotoes");
            DropForeignKey("dbo.Employees", "Shift_Id", "dbo.Shifts");
            DropForeignKey("dbo.Employees", "Position_Id", "dbo.Positions");
            DropForeignKey("dbo.Employees", "EmploymentStatus_Id", "dbo.EmploymentStatus");
            DropForeignKey("dbo.Employees", "Department_Id", "dbo.Departments");
            DropIndex("dbo.Employees", new[] { "TeamMemberPhoto_Id" });
            DropIndex("dbo.Employees", new[] { "Shift_Id" });
            DropIndex("dbo.Employees", new[] { "Position_Id" });
            DropIndex("dbo.Employees", new[] { "Manager_Id" });
            DropIndex("dbo.Employees", new[] { "EmploymentStatus_Id" });
            DropIndex("dbo.Employees", new[] { "Department_Id" });
            AlterColumn("dbo.Employees", "TeamMemberPhoto_Id", c => c.Int(nullable: false));
            AlterColumn("dbo.Employees", "Manager_Id", c => c.Int(nullable: false));
            AlterColumn("dbo.Employees", "Shift_Id", c => c.Int(nullable: false));
            AlterColumn("dbo.Employees", "EmploymentStatus_Id", c => c.Int(nullable: false));
            AlterColumn("dbo.Employees", "Department_Id", c => c.Int(nullable: false));
            AlterColumn("dbo.Employees", "Position_Id", c => c.Int(nullable: false));
            RenameColumn(table: "dbo.Employees", name: "TeamMemberPhoto_Id", newName: "TeamMemberPhotoId");
            RenameColumn(table: "dbo.Employees", name: "Shift_Id", newName: "ShiftId");
            RenameColumn(table: "dbo.Employees", name: "Position_Id", newName: "PositionId");
            RenameColumn(table: "dbo.Employees", name: "Manager_Id", newName: "ManagerId");
            RenameColumn(table: "dbo.Employees", name: "EmploymentStatus_Id", newName: "EmploymentStatusId");
            RenameColumn(table: "dbo.Employees", name: "Department_Id", newName: "DepartmentId");
            CreateIndex("dbo.Employees", "TeamMemberPhotoId");
            CreateIndex("dbo.Employees", "ManagerId");
            CreateIndex("dbo.Employees", "ShiftId");
            CreateIndex("dbo.Employees", "EmploymentStatusId");
            CreateIndex("dbo.Employees", "DepartmentId");
            CreateIndex("dbo.Employees", "PositionId");
            AddForeignKey("dbo.Employees", "TeamMemberPhotoId", "dbo.TeamMemberPhotoes", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Employees", "ShiftId", "dbo.Shifts", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Employees", "PositionId", "dbo.Positions", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Employees", "EmploymentStatusId", "dbo.EmploymentStatus", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Employees", "DepartmentId", "dbo.Departments", "Id", cascadeDelete: true);
        }
    }
}
