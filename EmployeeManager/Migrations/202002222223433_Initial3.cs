namespace EmployeeManager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial3 : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Employees", name: "Position_Id", newName: "PositionId");
            RenameIndex(table: "dbo.Employees", name: "IX_Position_Id", newName: "IX_PositionId");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.Employees", name: "IX_PositionId", newName: "IX_Position_Id");
            RenameColumn(table: "dbo.Employees", name: "PositionId", newName: "Position_Id");
        }
    }
}
