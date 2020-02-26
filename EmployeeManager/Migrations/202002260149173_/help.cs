namespace EmployeeManager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class help : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AuditLogs", "EntityName", c => c.String());
            AddColumn("dbo.AuditLogs", "PropertyName", c => c.String());
            AddColumn("dbo.AuditLogs", "PrimaryKeyValue", c => c.String());
            AddColumn("dbo.AuditLogs", "OldValue", c => c.String());
            AddColumn("dbo.AuditLogs", "DateChanged", c => c.DateTime(nullable: false));
            DropColumn("dbo.AuditLogs", "EventDateUTC");
            DropColumn("dbo.AuditLogs", "EventType");
            DropColumn("dbo.AuditLogs", "RecordId");
            DropColumn("dbo.AuditLogs", "ColumnName");
            DropColumn("dbo.AuditLogs", "OriginalValue");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AuditLogs", "OriginalValue", c => c.String());
            AddColumn("dbo.AuditLogs", "ColumnName", c => c.String());
            AddColumn("dbo.AuditLogs", "RecordId", c => c.String());
            AddColumn("dbo.AuditLogs", "EventType", c => c.String());
            AddColumn("dbo.AuditLogs", "EventDateUTC", c => c.DateTime(nullable: false));
            DropColumn("dbo.AuditLogs", "DateChanged");
            DropColumn("dbo.AuditLogs", "OldValue");
            DropColumn("dbo.AuditLogs", "PrimaryKeyValue");
            DropColumn("dbo.AuditLogs", "PropertyName");
            DropColumn("dbo.AuditLogs", "EntityName");
        }
    }
}
