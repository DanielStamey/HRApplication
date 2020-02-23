namespace EmployeeManager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial4 : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Employees", name: "Department_Id", newName: "DepartmentId");
            RenameColumn(table: "dbo.Employees", name: "EmploymentStatus_Id", newName: "EmploymentStatusId");
            RenameColumn(table: "dbo.Employees", name: "Manager_Id", newName: "ManagerId");
            RenameColumn(table: "dbo.Employees", name: "Shift_Id", newName: "ShiftId");
            RenameIndex(table: "dbo.Employees", name: "IX_Department_Id", newName: "IX_DepartmentId");
            RenameIndex(table: "dbo.Employees", name: "IX_EmploymentStatus_Id", newName: "IX_EmploymentStatusId");
            RenameIndex(table: "dbo.Employees", name: "IX_Shift_Id", newName: "IX_ShiftId");
            RenameIndex(table: "dbo.Employees", name: "IX_Manager_Id", newName: "IX_ManagerId");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.Employees", name: "IX_ManagerId", newName: "IX_Manager_Id");
            RenameIndex(table: "dbo.Employees", name: "IX_ShiftId", newName: "IX_Shift_Id");
            RenameIndex(table: "dbo.Employees", name: "IX_EmploymentStatusId", newName: "IX_EmploymentStatus_Id");
            RenameIndex(table: "dbo.Employees", name: "IX_DepartmentId", newName: "IX_Department_Id");
            RenameColumn(table: "dbo.Employees", name: "ShiftId", newName: "Shift_Id");
            RenameColumn(table: "dbo.Employees", name: "ManagerId", newName: "Manager_Id");
            RenameColumn(table: "dbo.Employees", name: "EmploymentStatusId", newName: "EmploymentStatus_Id");
            RenameColumn(table: "dbo.Employees", name: "DepartmentId", newName: "Department_Id");
        }
    }
}
