namespace EmployeeManager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial12 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Permissions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PermissionName = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.PermissionEmployees",
                c => new
                    {
                        Permission_Id = c.Int(nullable: false),
                        Employee_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Permission_Id, t.Employee_Id })
                .ForeignKey("dbo.Permissions", t => t.Permission_Id, cascadeDelete: true)
                .ForeignKey("dbo.Employees", t => t.Employee_Id, cascadeDelete: true)
                .Index(t => t.Permission_Id)
                .Index(t => t.Employee_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PermissionEmployees", "Employee_Id", "dbo.Employees");
            DropForeignKey("dbo.PermissionEmployees", "Permission_Id", "dbo.Permissions");
            DropIndex("dbo.PermissionEmployees", new[] { "Employee_Id" });
            DropIndex("dbo.PermissionEmployees", new[] { "Permission_Id" });
            DropTable("dbo.PermissionEmployees");
            DropTable("dbo.Permissions");
        }
    }
}
