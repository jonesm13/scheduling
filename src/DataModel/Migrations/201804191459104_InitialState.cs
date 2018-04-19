namespace DataModel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialState : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Schedule",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        StationId = c.Guid(nullable: false),
                        TemplateId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Station", t => t.StationId, cascadeDelete: true)
                .ForeignKey("dbo.Template", t => t.TemplateId, cascadeDelete: true)
                .Index(t => t.StationId)
                .Index(t => t.TemplateId);
            
            CreateTable(
                "dbo.Station",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Template",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.TemplateItem",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        TemplateId = c.Guid(nullable: false),
                        Type = c.Int(nullable: false),
                        Order = c.Int(nullable: false),
                        State = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Template", t => t.TemplateId, cascadeDelete: true)
                .Index(t => t.TemplateId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Schedule", "TemplateId", "dbo.Template");
            DropForeignKey("dbo.TemplateItem", "TemplateId", "dbo.Template");
            DropForeignKey("dbo.Schedule", "StationId", "dbo.Station");
            DropIndex("dbo.TemplateItem", new[] { "TemplateId" });
            DropIndex("dbo.Schedule", new[] { "TemplateId" });
            DropIndex("dbo.Schedule", new[] { "StationId" });
            DropTable("dbo.TemplateItem");
            DropTable("dbo.Template");
            DropTable("dbo.Station");
            DropTable("dbo.Schedule");
        }
    }
}
