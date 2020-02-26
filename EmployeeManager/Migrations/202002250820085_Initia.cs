namespace EmployeeManager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initia : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.PermissionEmployees", "Permission_Id", "dbo.Permissions");
            DropForeignKey("dbo.PermissionEmployees", "Employee_Id", "dbo.Employees");
            DropIndex("dbo.PermissionEmployees", new[] { "Permission_Id" });
            DropIndex("dbo.PermissionEmployees", new[] { "Employee_Id" });
            CreateTable(
                "dbo.EmployeePermissions",
                c => new
                    {
                        EmployeeId = c.Int(nullable: false),
                        PermissionId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.EmployeeId, t.PermissionId })
                .ForeignKey("dbo.Permissions", t => t.PermissionId, cascadeDelete: true)
                .ForeignKey("dbo.Employees", t => t.EmployeeId, cascadeDelete: true)
                .Index(t => t.EmployeeId)
                .Index(t => t.PermissionId);
            
            DropTable("dbo.PermissionEmployees");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.PermissionEmployees",
                c => new
                    {
                        Permission_Id = c.Int(nullable: false),
                        Employee_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Permission_Id, t.Employee_Id });
            
            DropForeignKey("dbo.EmployeePermissions", "EmployeeId", "dbo.Employees");
            DropForeignKey("dbo.EmployeePermissions", "PermissionId", "dbo.Permissions");
            DropIndex("dbo.EmployeePermissions", new[] { "PermissionId" });
            DropIndex("dbo.EmployeePermissions", new[] { "EmployeeId" });
            DropTable("dbo.EmployeePermissions");
            CreateIndex("dbo.PermissionEmployees", "Employee_Id");
            CreateIndex("dbo.PermissionEmployees", "Permission_Id");
            AddForeignKey("dbo.PermissionEmployees", "Employee_Id", "dbo.Employees", "Id", cascadeDelete: true);
            AddForeignKey("dbo.PermissionEmployees", "Permission_Id", "dbo.Permissions", "Id", cascadeDelete: true);
        }
    }
}
