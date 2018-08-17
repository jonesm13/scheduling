namespace DataModel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddingScheduleOverride : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ScheduleOverride",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        StationId = c.Guid(nullable: false),
                        TemplateId = c.Guid(nullable: false),
                        Date = c.DateTime(nullable: false),
                        Start = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Station", t => t.StationId, cascadeDelete: true)
                .ForeignKey("dbo.Template", t => t.TemplateId, cascadeDelete: true)
                .Index(t => t.StationId)
                .Index(t => t.TemplateId);
            
            AddColumn("dbo.Schedule", "Days", c => c.Int(nullable: false));
            AddColumn("dbo.Schedule", "Start", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ScheduleOverride", "TemplateId", "dbo.Template");
            DropForeignKey("dbo.ScheduleOverride", "StationId", "dbo.Station");
            DropIndex("dbo.ScheduleOverride", new[] { "TemplateId" });
            DropIndex("dbo.ScheduleOverride", new[] { "StationId" });
            DropColumn("dbo.Schedule", "Start");
            DropColumn("dbo.Schedule", "Days");
            DropTable("dbo.ScheduleOverride");
        }
    }
}
